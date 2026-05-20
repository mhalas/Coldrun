using Application.Requests.Trucks;
using Domain.Entities;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Application.Handlers.Trucks
{
    public class GetTruckListHandler(ColdrunContext dbContext) : IRequestHandler<GetTruckListRequest, IEnumerable<Truck>>
    {
        private const int DefaultPageSize = 50;
        private const int DefaultPageNumber = 1;

        public async Task<IEnumerable<Truck>> Handle(GetTruckListRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Truck> query = dbContext.Truck;

            query = FilterBy(request, query);
            query = SortBy(request, query);
            query = Pagination(request, query);

            return await query.ToListAsync(cancellationToken);
        }

        private IQueryable<Truck> Pagination(GetTruckListRequest request, IQueryable<Truck> query)
        {
            var pageNumber = request.PageNumber ?? DefaultPageNumber;
            var pageSize = request.PageSize ?? DefaultPageSize;

            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        private static IQueryable<Truck> FilterBy(GetTruckListRequest request, IQueryable<Truck> query)
        {
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(searchTerm) ||
                                         x.Code.ToLower().Contains(searchTerm) ||
                                         !string.IsNullOrWhiteSpace(x.Description) && x.Description.ToLower().Contains(searchTerm));
            }

            if (request.Status.HasValue)
            {
                var filteredStatus = (int)request.Status.Value;
                query = query.Where(x => x.Status == filteredStatus);
            }

            return query;
        }

        private static IQueryable<Truck> SortBy(GetTruckListRequest request, IQueryable<Truck> query)
        {
            var sortBy = request.SortBy ?? "Id";
            string sortDirection = request.IsDescending ? "descending" : "ascending";

            string ordering = $"{sortBy} {sortDirection}";
            return query.OrderBy(ordering);
        }
    }
}
