using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Trucks
{
    public class RemoveTruckRequest : IRequest, IId
    {
        [Required]
        public int Id { get; set; }
    }
}
