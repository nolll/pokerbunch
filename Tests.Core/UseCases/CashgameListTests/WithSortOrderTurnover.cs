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
            var result = Sut.Execute(Request);

            Assert.AreEqual(CashgameList.SortOrder.Turnover, result.SortOrder);
            Assert.AreEqual(600, result.List[0].Turnover.Amount);
            Assert.AreEqual(400, result.List[1].Turnover.Amount);
        }
    }
}