using Application.Requests.Trucks;
using Domain.Entities;
using Infrastructure;
using MediatR;

namespace Application.Handlers.Trucks
{
    public class CreateTruckHandler(ColdrunContext dbContext) : IRequestHandler<CreateTruckRequest, int>
    {
        public async Task<int> Handle(CreateTruckRequest request, CancellationToken cancellationToken)
        {
            var newTruck = new Truck
            {
                Name = request.Name,
                Code = request.Code,
                Description = request.Description,
                Status = (int)request.Status,
            };

            var result = await dbContext.Truck.AddAsync(newTruck, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return result.Entity.Id;
        }
    }
}
