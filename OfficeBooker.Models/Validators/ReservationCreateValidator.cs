using FluentValidation;
using OfficeBooker.Models.DTOs;

namespace OfficeBooker.Models.Validators
{
    public class ReservationCreateValidator : AbstractValidator<ReservationCreateDTO>
    {
        public ReservationCreateValidator()
        {
            RuleFor(x=>x.OfficeId)
                .GreaterThan(0)
                .WithMessage("OfficeId must be greater than 0.");


            RuleFor(x => x.ReservationStartTime)
            .NotEmpty()
            .GreaterThan(DateTime.Now).WithMessage("Reservation cannot start in the past.");

            RuleFor(x => x.ReservationEndTime)
                .NotEmpty()
                .GreaterThan(x => x.ReservationStartTime).WithMessage("Reservation end time must be after the start time.");
        }
    }
}
