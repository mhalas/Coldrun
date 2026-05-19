using Domain.Enums;

namespace Domain.Extensions
{
    public static class TruckExtensions
    {
        public static bool CanTransitionTo(this TruckStatus oldStatus, TruckStatus newStatus)
        {
            if (oldStatus == TruckStatus.OutOfService || newStatus == TruckStatus.OutOfService)
            {
                return true;
            }

            if (oldStatus == TruckStatus.Returning && newStatus == TruckStatus.Loading)
            {
                return true;
            }

            if (newStatus == oldStatus + 1)
            {
                return true;
            }

            return false;
        }
    }
}
