using Domain.Enums;

namespace Application.Requests.Trucks
{
    public interface IStatus
    {
        public TruckStatus Status { get; }
    }
}
