using Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Trucks
{
    public class CreateTruckRequest: IRequest<int>, ICode, IStatus
    {
        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public TruckStatus Status { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
