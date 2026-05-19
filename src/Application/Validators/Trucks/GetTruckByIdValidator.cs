using Application.Requests.Trucks;
using FluentValidation;
using Infrastructure;

namespace Application.Validators.Trucks
{
    public class GetTruckByIdValidator : AbstractValidator<GetTruckByIdRequest>
    {
        public GetTruckByIdValidator(ColdrunContext dbContext)
        {
            Include(new TruckExistsValidator<GetTruckByIdRequest>(dbContext));
        }
    }
}
