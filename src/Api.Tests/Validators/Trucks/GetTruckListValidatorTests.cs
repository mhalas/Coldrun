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

        [Test]
        public async Task Validate_GetTruckList_ShouldPassValidation_WhenPageNumberIsGreaterThan0()
        {
            var request = new GetTruckListRequest()
            {
                PageNumber = 1
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task Validate_GetTruckList_ShouldThrowError_WhenPageNumberLessThan0(int pageNumber)
        {
            var request = new GetTruckListRequest()
            {
                PageNumber = pageNumber
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x.PageNumber)
                .WithErrorMessage("Page number must be at least 1.");
        }

        [Test]
        public async Task Validate_GetTruckList_ShouldPassValidation_WhenPageSizeIsGreaterThan0()
        {
            var request = new GetTruckListRequest()
            {
                PageSize = 1
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task Validate_GetTruckList_ShouldThrowError_WhenPageSizeLessThan1(int pageSize)
        {
            var request = new GetTruckListRequest()
            {
                PageSize = pageSize
            };


            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x.PageSize)
                .WithErrorMessage("Page size must be at least 1.");
        }
    }
}
