using Domain.Extensions;
using Domain.Enums;

namespace Api.Tests.Extensions
{
    [TestFixture]
    public class TruckExtensions
    {
        [TestCase(TruckStatus.Loading, TruckStatus.ToJob, true)]
        [TestCase(TruckStatus.ToJob, TruckStatus.AtJob, true)]
        [TestCase(TruckStatus.AtJob, TruckStatus.Returning, true)]

        [TestCase(TruckStatus.Returning, TruckStatus.Loading, true)]

        [TestCase(TruckStatus.Loading, TruckStatus.OutOfService, true)]
        [TestCase(TruckStatus.ToJob, TruckStatus.OutOfService, true)]
        [TestCase(TruckStatus.AtJob, TruckStatus.OutOfService, true)]
        [TestCase(TruckStatus.Returning, TruckStatus.OutOfService, true)]
        [TestCase(TruckStatus.OutOfService, TruckStatus.Loading, true)]
        [TestCase(TruckStatus.OutOfService, TruckStatus.ToJob, true)]
        [TestCase(TruckStatus.OutOfService, TruckStatus.AtJob, true)]
        [TestCase(TruckStatus.OutOfService, TruckStatus.Returning, true)]

        [TestCase(TruckStatus.Loading, TruckStatus.AtJob, false)]
        [TestCase(TruckStatus.Loading, TruckStatus.Returning, false)]
        [TestCase(TruckStatus.ToJob, TruckStatus.Loading, false)]
        [TestCase(TruckStatus.ToJob, TruckStatus.Returning, false)]
        [TestCase(TruckStatus.Returning, TruckStatus.ToJob, false)]
        [TestCase(TruckStatus.Returning, TruckStatus.AtJob, false)]
        public void Status_Transitions_Should_Follow_Business_Rules(
        TruckStatus currentStatus,
        TruckStatus newStatus,
        bool expectedResult)
        {
            Assert.That(currentStatus.CanTransitionTo(newStatus), Is.EqualTo(expectedResult));
        }
    }
}
