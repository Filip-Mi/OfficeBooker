using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeBooker.Models.Validators
{
    ///<summary>
    /// Validator for the OfficeCreateDTO class, ensuring that all required fields are properly validated.
    /// </summary>
    public class CreateOfficeValidator : AbstractValidator<DTOs.OfficeCreateDTO>
    {
        /// <summary>
        /// Validation rules for creating a new office space, including checks for capacity, floor number, and office number.
        /// </summary>
        public CreateOfficeValidator()
        {
            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Capacity must be greater than 0.");
            RuleFor(x => x.FloorNumber)
                .GreaterThanOrEqualTo(0).WithMessage("Floor number cannot be negative.");
            RuleFor(x => x.OfficeNumber)
                .GreaterThan(0).WithMessage("Office number must be greater than 0.");
        }
    }
}
