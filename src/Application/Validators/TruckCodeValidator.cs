using Application.Requests.Trucks;
using FluentValidation;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Validators
{
    public class TruckCodeValidator<T>: AbstractValidator<T> where T : ICode
    {
        private readonly ColdrunContext _dbContext;

        public TruckCodeValidator(ColdrunContext context)
        {
            _dbContext = context;

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MustAsync(BeUniqueCode).WithMessage("A truck with this code already exists.");
        }

        private async Task<bool> BeUniqueCode(T request, string code, CancellationToken cancellationToken)
        {
            if (request is UpdateTruckRequest updateTruckRequest)
            {
                return !await _dbContext.Truck
                    .AnyAsync(t => t.Code == code && t.Id != updateTruckRequest.Id, cancellationToken);
            }

            return !await _dbContext.Truck
                .AnyAsync(t => t.Code == code, cancellationToken);
        }
    }
}
