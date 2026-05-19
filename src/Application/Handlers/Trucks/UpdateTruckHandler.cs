using Application.Requests.Trucks;
using Infrastructure;
using MediatR;

namespace Application.Handlers.Trucks
{
    public class UpdateTruckHandler(ColdrunContext dbContext) : IRequestHandler<UpdateTruckRequest>
    {
        public async Task Handle(UpdateTruckRequest request, CancellationToken cancellationToken)
        {
            var entityToUpdate = await dbContext.Truck.FindAsync([request.Id], cancellationToken);

            entityToUpdate.Name = request.Name;
            entityToUpdate.Code = request.Code;
            entityToUpdate.Description = request.Description;
            entityToUpdate.Status = (int)request.Status;

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
