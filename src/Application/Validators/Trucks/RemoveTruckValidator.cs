using Application.Requests.Trucks;
using FluentValidation;
using Infrastructure;

namespace Application.Validators.Trucks
{
    public class RemoveTruckValidator : AbstractValidator<RemoveTruckRequest>
    {
        public RemoveTruckValidator(ColdrunContext dbContext)
        {
            Include(new TruckExistsValidator<RemoveTruckRequest>(dbContext));
        }
    }
}
