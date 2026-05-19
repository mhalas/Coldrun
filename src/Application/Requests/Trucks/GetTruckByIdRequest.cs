using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Trucks
{
    public class GetTruckByIdRequest: IRequest<Truck>, IId
    {
        [Required]
        public int Id { get; set; }
    }
}
