using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class TruckDto
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
