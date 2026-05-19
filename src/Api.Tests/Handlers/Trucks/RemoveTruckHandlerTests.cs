using Application.Handlers.Trucks;
using Application.Requests.Trucks;
using Domain.Entities;
using Domain.Enums;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api.Tests.Handlers.Trucks
{
    [TestFixture]
    public class RemoveTruckHandlerTests
    {
        private ColdrunContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ColdrunContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ColdrunContext(options);
        }

        [Test]
        public async Task Handle_ShouldNotThrowsExceptions_WhenTruckWithIdExists()
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

                var handler = new RemoveTruckHandler(context);
                var request = new RemoveTruckRequest()
                {
                    Id = 2
                };

                await handler.Handle(request, cancellationToken);

                var existingIds = context.Truck.Select(x => x.Id).ToList();
                Assert.That(existingIds, Is.EquivalentTo([1,3]));
            }
        }
    }
}
