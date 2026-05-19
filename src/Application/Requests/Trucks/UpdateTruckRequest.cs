using Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Trucks
{
    public class UpdateTruckRequest : IRequest, IId, ICode
    {
        [Required]
        public required int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Code { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
        [Required]
        public required TruckStatus Status { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
