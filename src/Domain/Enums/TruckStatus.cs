namespace Domain.Enums
{
    public enum TruckStatus
    {
        OutOfService,
        /// <summary>
        /// Available statuses: OutOfService, ToJob
        /// </summary>
        Loading,
        /// <summary>
        /// Available statuses: OutOfService, AtJob
        /// </summary>
        ToJob,
        /// <summary>
        /// Available statuses: OutOfService, Returning
        /// </summary>
        AtJob,
        /// <summary>
        /// Available statuses: OutOfService, Loading
        /// </summary>
        Returning
    }
}
