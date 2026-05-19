using Application.Requests.Trucks;
using FluentValidation;

namespace Application.Validators.Trucks
{
    public class GetTruckListValidator : AbstractValidator<GetTruckListRequest>
    {
        private readonly List<string> _supportedSortByColumns = ["id", "name", "code", "status", "description"];

        public GetTruckListValidator()
        {
            RuleFor(x => x.SortBy)
                .Must(HaveValidSortBy)
                .WithMessage(x => $"Sorting by column '{x.SortBy}' is not supported.")
                .When(x => !string.IsNullOrWhiteSpace(x.SortBy));
        }

        private bool HaveValidSortBy(string sortBy)
        {
            return _supportedSortByColumns.Contains(sortBy.ToLower());
        }
    }
}
