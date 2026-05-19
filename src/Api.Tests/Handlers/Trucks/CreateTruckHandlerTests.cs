using Application.Handlers.Trucks;
using Application.Requests.Trucks;
using Domain.Entities;
using Domain.Enums;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api.Tests.Handlers.Trucks
{
    [TestFixture]
    public class CreateTruckHandlerTests
    {
        private ColdrunContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ColdrunContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ColdrunContext(options);
        }

        [Test]
        public async Task Handle_ShouldReturnId_WhenTruckWasAddedSuccessfully()
        {
            var cancellationToken = new CancellationToken();
            using (var context = GetDbContext())
            {
                await context.Truck.AddAsync(new Truck
                {
                    Id = 1,
                    Code = "Code 1",
                    Description = "Description 1",
                    Name = "Name 1",
                    Status = (int)TruckStatus.OutOfService
                }, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                var request = new CreateTruckRequest()
                {
                    Code = "Code 2",
                    Description = "Description 2",
                    Name = "Name 2",
                    Status = TruckStatus.OutOfService
                };

                var handler = new CreateTruckHandler(context);
                var newId = await handler.Handle(request, cancellationToken);
                Assert.That(newId, Is.EqualTo(2));

                var addedEntity = context.Truck.FindAsync([newId], cancellationToken).Result;
                Assert.That(addedEntity, Is.EqualTo(new Truck()
                {
                    Id = 2,
                    Code = "Code 2",
                    Description = "Description 2",
                    Name = "Name 2",
                    Status = (int)TruckStatus.OutOfService
                }).UsingPropertiesComparer());
            }
        }
    }
}
