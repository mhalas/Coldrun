using Application.Requests.Trucks;
using Domain.Enums;
using Domain.Extensions;
using FluentValidation;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Validators.Trucks
{
    public class UpdateTruckValidator : AbstractValidator<UpdateTruckRequest>
    {
        private readonly ColdrunContext _dbContext;

        public UpdateTruckValidator(ColdrunContext dbContext)
        {
            _dbContext = dbContext;

            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x)
                .SetValidator(new TruckExistsValidator<UpdateTruckRequest>(dbContext))
                .DependentRules(() =>
                {
                    RuleFor(x => x)
                        .SetValidator(new TruckCodeValidator<UpdateTruckRequest>(dbContext));

                    RuleFor(x => x.Status)
                        .MustAsync(CheckStatusTransition)
                        .WithMessage("This status transition is not allowed according to the business rules.");
                });
        }

        private async Task<bool> CheckStatusTransition(UpdateTruckRequest request, TruckStatus status, CancellationToken cancellationToken)
        {
            var currentStatus = await _dbContext.Truck
                    .Where(t => t.Id == request.Id)
                    .Select(t => (TruckStatus?)t.Status)
                    .FirstOrDefaultAsync(cancellationToken);

            if (currentStatus == null) 
                return false;

            return currentStatus.Value.CanTransitionTo(status);
        }
    }
}
