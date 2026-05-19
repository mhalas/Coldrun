using Application.Requests.Trucks;
using Application.Validators.Trucks;
using FluentValidation.TestHelper;

namespace Api.Tests.Validators.Trucks
{
    [TestFixture]
    public class GetTruckListValidatorTests
    {
        private GetTruckListValidator _validator;

        [SetUp]
        public async Task Setup()
        {
            _validator = new GetTruckListValidator();
        }

        [TestCase("id")]
        [TestCase("name")]
        [TestCase("code")]
        [TestCase("status")]
        [TestCase("description")]
        public async Task Validate_GetTruckList_ShouldPassValidation_WhenPassingSupportedSortBy(
            string sortBy)
        {
            var request = new GetTruckListRequest()
            {
                SortBy = sortBy
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public async Task Validate_GetTruckList_ShouldThrowError_WhenPassingNotSupportedSortBy()
        {
            const string passedSortBy = "notsupportedsortby";

            var request = new GetTruckListRequest()
            {
                SortBy = passedSortBy
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x.SortBy)
                .WithErrorMessage($"Sorting by column '{passedSortBy}' is not supported.");
        }
    }
}
