using Application.Requests.Trucks;
using FluentValidation;

namespace Application.Validators
{
    public class TruckStatusValidator<T> : AbstractValidator<T> where T : IStatus
    {
        public TruckStatusValidator() 
        {
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("A correct status is required.");
        }
    }
}
