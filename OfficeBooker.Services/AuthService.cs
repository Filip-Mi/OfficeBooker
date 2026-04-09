using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OfficeBooker.Models;
using OfficeBooker.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OfficeBooker.Services.IServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Worker> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IValidator<LoginWorkerDTO> _loginWorkerValidator;
        public AuthService(UserManager<Worker> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration , IValidator<LoginWorkerDTO> loginWorkerValidator )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _loginWorkerValidator = loginWorkerValidator;
        }
        public async Task<object?> GetCurrentUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            return new
            {
                user.Email,
                user.Name,
                user.Surname,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }

        public async Task<AuthResponseDTO?> LoginAsync(LoginWorkerDTO model)
        {
            //Validation
            var validationResult = await _loginWorkerValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            //Buisness Logic
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password)) throw new UnauthorizedAccessException("Invalid email or password.");

            // Token Generation
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
            foreach (var role in userRoles) authClaims.Add(new Claim(ClaimTypes.Role, role));

            var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
            );

            return new AuthResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }

        public async Task<(bool Success, string Message)> RegisterAsync(CreateWorkerDTO model)
        {
            var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null) return (false, "User already exist");

            var worker = new Worker { UserName = model.Email, Name = model.Name, Surname = model.Surname, Email = model.Email };
            var result = await _userManager.CreateAsync(worker, model.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(model.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));
                }
                await _userManager.AddToRoleAsync(worker, model.Role);
                return (true, "Registration Successful");
            }
            return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
