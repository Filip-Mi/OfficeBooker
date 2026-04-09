using FluentValidation;
using OfficeBooker.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OfficeBooker.Models.Validators
{
    public class LoginWorkerValidator:AbstractValidator<LoginWorkerDTO>
    {
        public LoginWorkerValidator() {

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.");
        }
    }
}
