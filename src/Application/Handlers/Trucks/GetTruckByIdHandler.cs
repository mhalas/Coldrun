using Application.Requests.Trucks;
using Domain.Entities;
using Infrastructure;
using MediatR;

namespace Application.Handlers.Trucks
{
    public class GetTruckByIdHandler(ColdrunContext dbContext) : IRequestHandler<GetTruckByIdRequest, Truck>
    {
        public async Task<Truck> Handle(GetTruckByIdRequest request, CancellationToken cancellationToken)
        {
            return await dbContext.Truck.FindAsync([request.Id], cancellationToken);
        }
    }
}
