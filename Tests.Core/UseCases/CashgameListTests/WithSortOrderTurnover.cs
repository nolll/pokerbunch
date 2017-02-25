using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameListTests
{
    public class WithSortOrderTurnover : Arrange
    {
        protected override CashgameList.SortOrder SortOrder => CashgameList.SortOrder.Turnover;

        [Test]
        public void HighestTurnoverIsFirst()
        {
            Assert.AreEqual(CashgameList.SortOrder.Turnover, Result.SortOrder);
            Assert.AreEqual(600, Result.List[0].Turnover.Amount);
            Assert.AreEqual(400, Result.List[1].Turnover.Amount);
        }
    }
}