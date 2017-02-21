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
            var result = Sut.Execute(Request);

            Assert.AreEqual(CashgameList.SortOrder.Duration, result.SortOrder);
            Assert.AreEqual(122, result.List[0].Duration.Minutes);
            Assert.AreEqual(62, result.List[1].Duration.Minutes);
        }
    }
}