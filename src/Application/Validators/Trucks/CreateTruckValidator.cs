using Application.Requests.Trucks;
using FluentValidation;
using Infrastructure;

namespace Application.Validators.Trucks
{
    public class CreateTruckValidator : AbstractValidator<CreateTruckRequest>
    {
        public CreateTruckValidator(ColdrunContext dbContext)
        {
            Include(new TruckCodeValidator<CreateTruckRequest>(dbContext));
        }
    }
}
