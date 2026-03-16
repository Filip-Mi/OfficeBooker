using OfficeBooker.Models.cs.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeBooker.Services.IServices
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> RegisterAsync(CreateWorkerDTO model);
        Task<AuthResponseDTO?> LoginAsync(LoginWorkerDTO model);
        Task<object?> GetCurrentUserAsync(string userId);
    }
}
