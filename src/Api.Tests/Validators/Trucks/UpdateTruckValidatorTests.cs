using Application.Requests.Trucks;
using Application.Validators;
using Application.Validators.Trucks;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.TestHelper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api.Tests.Validators.Trucks
{
    [TestFixture]
    public class UpdateTruckValidatorTests
    {
        private CancellationToken _cancellationToken;
        private ColdrunContext _context;
        private UpdateTruckValidator _validator;

        [SetUp]
        public async Task Setup()
        {
            _cancellationToken = new CancellationToken();
            var options = new DbContextOptionsBuilder<ColdrunContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ColdrunContext(options);

            _validator = new UpdateTruckValidator(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [TestCase(TruckStatus.Loading, TruckStatus.AtJob)]
        [TestCase(TruckStatus.Loading, TruckStatus.Returning)]
        [TestCase(TruckStatus.ToJob, TruckStatus.Loading)]
        [TestCase(TruckStatus.ToJob, TruckStatus.Returning)]
        [TestCase(TruckStatus.Returning, TruckStatus.ToJob)]
        [TestCase(TruckStatus.Returning, TruckStatus.AtJob)]
        public async Task Validate_UpdateTruck_ShouldHaveError_WhenNewStatusIsIncorrect(
            TruckStatus currentStatus, 
            TruckStatus newStatus)
        {
            await _context.Truck.AddAsync(new Truck
                {
                    Id = 1,
                    Code = "Code 1",
                    Description = "Description 1",
                    Name = "Name 1",
                    Status = (int)currentStatus
            }, _cancellationToken);
            await _context.SaveChangesAsync(_cancellationToken);

            var request = new UpdateTruckRequest()
            {
                Id = 1,
                Code = "Code 1",
                Description = "Description 1",
                Name = "Name 1",
                Status = newStatus
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x.Status)
                .WithErrorMessage("This status transition is not allowed according to the business rules.");
        }

        [TestCase(TruckStatus.Loading, TruckStatus.ToJob)]
        [TestCase(TruckStatus.ToJob, TruckStatus.AtJob)]
        [TestCase(TruckStatus.AtJob, TruckStatus.Returning)]
        [TestCase(TruckStatus.Returning, TruckStatus.Loading)]
        [TestCase(TruckStatus.Loading, TruckStatus.OutOfService)]
        [TestCase(TruckStatus.ToJob, TruckStatus.OutOfService)]
        [TestCase(TruckStatus.AtJob, TruckStatus.OutOfService)]
        [TestCase(TruckStatus.Returning, TruckStatus.OutOfService)]
        [TestCase(TruckStatus.OutOfService, TruckStatus.Loading)]
        [TestCase(TruckStatus.OutOfService, TruckStatus.ToJob)]
        [TestCase(TruckStatus.OutOfService, TruckStatus.AtJob)]
        [TestCase(TruckStatus.OutOfService, TruckStatus.Returning)]
        public async Task Validate_UpdateTruck_ShouldPassValidation_WhenNewStatusIsCorrect(
            TruckStatus currentStatus,
            TruckStatus newStatus)
        {
            await _context.Truck.AddAsync(new Truck
            {
                Id = 1,
                Code = "Code 1",
                Description = "Description 1",
                Name = "Name 1",
                Status = (int)currentStatus
            }, _cancellationToken);
            await _context.SaveChangesAsync(_cancellationToken);

            var request = new UpdateTruckRequest()
            {
                Id = 1,
                Code = "Code 1",
                Description = "Description 1",
                Name = "Name 1",
                Status = newStatus
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
