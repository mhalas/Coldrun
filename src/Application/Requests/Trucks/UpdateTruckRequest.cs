using Application.Dtos;
using Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Trucks
{
    public class UpdateTruckRequest : IRequest, IId, ICode, IStatus
    {
        [Required]
        public int Id { get; set; }
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

        public static UpdateTruckRequest MapFromDto(int id, TruckDto dto)
        {
            return new UpdateTruckRequest
            {
                Id = id,
                Code = dto.Code,
                Name = dto.Name,
                Status = dto.Status,
                Description = dto.Description
            };
        }
    }
}
