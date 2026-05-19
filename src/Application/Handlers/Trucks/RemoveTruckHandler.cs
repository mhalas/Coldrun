using Application.Requests.Trucks;
using Infrastructure;
using MediatR;

namespace Application.Handlers.Trucks
{
    public class RemoveTruckHandler(ColdrunContext dbContext) : IRequestHandler<RemoveTruckRequest>
    {
        public async Task Handle(RemoveTruckRequest request, CancellationToken cancellationToken)
        {
            var entityToRemove = await dbContext.Truck.FindAsync([request.Id], cancellationToken);
            dbContext.Remove(entityToRemove);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
