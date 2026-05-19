using Application.Handlers.Trucks;
using Application.Requests.Trucks;
using Domain.Entities;
using Domain.Enums;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api.Tests.Handlers.Trucks
{
    [TestFixture]
    public class UpdateTruckHandlerTests
    {
        private ColdrunContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ColdrunContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ColdrunContext(options);
        }

        [Test]
        public async Task Handle_ShouldNotThrowsExceptions_WhenTruckWasUpdated()
        {
            var cancellationToken = new CancellationToken();
            using (var context = GetDbContext())
            {
                await context.Truck.AddRangeAsync([
                    new Truck
                    {
                        Id = 1,
                        Code = "Code 1",
                        Description = "Description 1",
                        Name = "Name 1",
                        Status = (int)TruckStatus.OutOfService
                    },
                    new Truck
                    {
                        Id = 2,
                        Code = "Code 2",
                        Description = "Description 2",
                        Name = "Name 2",
                        Status = (int)TruckStatus.Loading
                    },
                    new Truck
                    {
                        Id = 3,
                        Code = "Code 3",
                        Description = "Description 3",
                        Name = "Name 3",
                        Status = (int)TruckStatus.ToJob
                    }], cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                var handler = new UpdateTruckHandler(context);
                var request = new UpdateTruckRequest()
                {
                    Id = 2,
                    Code = "Updated Code 2",
                    Description = "Updated Description 2",
                    Name = "Updated Name 2",
                    Status = TruckStatus.ToJob,
                    
                };

                await handler.Handle(request, cancellationToken);

                var updatedTruck = await context.Truck.SingleAsync(x => x.Id == 2);
                Assert.That(updatedTruck, Is.EqualTo(new Truck()
                {
                    Id = 2,
                    Code = "Updated Code 2",
                    Description = "Updated Description 2",
                    Name = "Updated Name 2",
                    Status = (int)TruckStatus.ToJob,
                }).UsingPropertiesComparer());
            }
        }
    }
}
