using Application.Requests.Trucks;
using Application.Validators;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace Api.Tests.Validators
{
    [TestFixture]
    public class TruckStatusValidatorTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task Validate_CreateTruck_ShouldPassValidation_WhenStatusCodeIsCorrect(int correctStatus)
        {
            var request = new CreateTruckRequest()
            {
                Status = (TruckStatus)correctStatus
            };

            var validator = new TruckStatusValidator<CreateTruckRequest>();
            var result = await validator.TestValidateAsync(request);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public async Task Validate_CreateTruck_ShouldThrowError_WhenStatusCodeIsOutOfRange()
        {
            var request = new CreateTruckRequest()
            {
                Status = (TruckStatus)999
            };

            var validator = new TruckStatusValidator<CreateTruckRequest>();
            var result = await validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x.Status)
                .WithErrorMessage("A correct status is required.");
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task Validate_UpdateTruck_ShouldPassValidation_WhenStatusCodeIsCorrect(int correctStatus)
        {
            var request = new UpdateTruckRequest()
            {
                Status = (TruckStatus)correctStatus
            };

            var validator = new TruckStatusValidator<UpdateTruckRequest>();
            var result = await validator.TestValidateAsync(request);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public async Task Validate_UpdateTruck_ShouldThrowError_WhenStatusCodeIsOutOfRange()
        {
            var request = new UpdateTruckRequest()
            {
                Status = (TruckStatus)999
            };

            var validator = new TruckStatusValidator<UpdateTruckRequest>();
            var result = await validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x.Status)
                .WithErrorMessage("A correct status is required.");
        }
    }
}
