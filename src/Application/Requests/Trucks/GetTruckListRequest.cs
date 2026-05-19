using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Requests.Trucks
{
    public class GetTruckListRequest : PaginationQuery, IRequest<IEnumerable<Truck>>
    {
        public string? SearchTerm { get; set; }
        public TruckStatus? Status { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; }
    }
}
