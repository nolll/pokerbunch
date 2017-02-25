using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameListTests
{
    public class WithSortOrderDuration : Arrange
    {
        protected override CashgameList.SortOrder SortOrder => CashgameList.SortOrder.Duration;

        [Test]
        public void HighestDurationIsFirst()
        {
            Assert.AreEqual(CashgameList.SortOrder.Duration, Result.SortOrder);
            Assert.AreEqual(122, Result.List[0].Duration.Minutes);
            Assert.AreEqual(62, Result.List[1].Duration.Minutes);
        }
    }
}