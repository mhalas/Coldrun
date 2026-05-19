using Application.Requests.Trucks;
using Application.Validators;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.TestHelper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api.Tests.Validators
{
    [TestFixture]
    public class TruckCodeValidatorTests
    {
        private CancellationToken _cancellationToken;
        private ColdrunContext _context;
        private TruckCodeValidator<ICode> _validator;

        [SetUp]
        public async Task Setup()
        {
            _cancellationToken = new CancellationToken();
            var options = new DbContextOptionsBuilder<ColdrunContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ColdrunContext(options);

            _validator = new TruckCodeValidator<ICode>(_context);

            await _context.Truck.AddRangeAsync([
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
                    }], _cancellationToken);
            await _context.SaveChangesAsync(_cancellationToken);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task Validate_CreateTruck_ShouldHaveError_WhenCodeAlreadyExists()
        {
            var request = new CreateTruckRequest()
            {
                Code = "Code 1",
                Description = "New Description",
                Name = "New Name",
                Status = TruckStatus.Loading
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x.Code)
                .WithErrorMessage("A truck with this code already exists.");
        }

        [Test]
        public async Task Validate_CreateTruck_ShouldPassValidation_WhenCodeIsUnique()
        {
            var request = new CreateTruckRequest()
            {
                Code = "New code",
                Description = "New Description",
                Name = "New Name",
                Status = TruckStatus.Loading
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public async Task Validate_UpdateTruck_ShouldHaveError_WhenCodeAlreadyExists()
        {
            var request = new UpdateTruckRequest()
            {
                Id = 2,
                Code = "Code 3",
                Description = "New Description",
                Name = "New Name",
                Status = TruckStatus.Loading
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x.Code)
                .WithErrorMessage("A truck with this code already exists.");
        }

        [Test]
        public async Task Validate_UpdateTruck_ShouldPassValidation_WhenCodeIsUnique()
        {
            var request = new UpdateTruckRequest()
            {
                Id = 2,
                Code = "New code",
                Description = "New Description",
                Name = "New Name",
                Status = TruckStatus.Loading
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
