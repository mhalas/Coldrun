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
    public class TruckExistsValidatorTests
    {
        private CancellationToken _cancellationToken;
        private ColdrunContext _context;
        private TruckExistsValidator<IId> _validator;

        [SetUp]
        public async Task Setup()
        {
            _cancellationToken = new CancellationToken();
            var options = new DbContextOptionsBuilder<ColdrunContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ColdrunContext(options);

            _validator = new TruckExistsValidator<IId>(_context);

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
        public async Task Validate_UpdateTruck_ShouldHaveError_WhenIdCouldNotFind()
        {
            var request = new UpdateTruckRequest()
            {
                Id = 100,
                Code = "New code",
                Description = "New Description",
                Name = "New Name",
                Status = TruckStatus.Loading
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("Truck with the specified Id does not exist.");
        }

        [Test]
        public async Task Validate_UpdateTruck_ShouldPassValidation_WhenIdExists()
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

        [Test]
        public async Task Validate_RemoveTruck_ShouldHaveError_WhenIdCouldNotFind()
        {
            var request = new RemoveTruckRequest()
            {
                Id = 100
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("Truck with the specified Id does not exist.");
        }

        [Test]
        public async Task Validate_RemoveTruck_ShouldPassValidation_WhenIdExists()
        {
            var request = new RemoveTruckRequest()
            {
                Id = 2
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public async Task Validate_GetTruckById_ShouldHaveError_WhenIdCouldNotFind()
        {
            var request = new GetTruckByIdRequest()
            {
                Id = 100
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("Truck with the specified Id does not exist.");
        }

        [Test]
        public async Task Validate_GetTruckById_ShouldPassValidation_WhenIdExists()
        {
            var request = new GetTruckByIdRequest()
            {
                Id = 2
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
