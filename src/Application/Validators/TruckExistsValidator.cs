using Application.Requests.Trucks;
using FluentValidation;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Validators
{
    public class TruckExistsValidator<T>: AbstractValidator<T> where T: IId
    {
        private readonly ColdrunContext _dbContext;

        public TruckExistsValidator(ColdrunContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .MustAsync(ExistInDatabase).WithMessage("Truck with the specified Id does not exist.");
        }

        public async Task<bool> ExistInDatabase(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Truck.AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}
