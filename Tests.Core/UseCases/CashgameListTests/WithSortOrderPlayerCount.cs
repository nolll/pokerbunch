using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameListTests
{
    public class WithSortOrderPlayerCount : Arrange
    {
        protected override CashgameList.SortOrder SortOrder => CashgameList.SortOrder.PlayerCount;

        [Test]
        public void TopList_SortByPlayerCount_HighestPlayerCountIsFirst()
        {
            Assert.AreEqual(CashgameList.SortOrder.PlayerCount, Result.SortOrder);
            Assert.AreEqual(2, Result.List[0].PlayerCount);
            Assert.AreEqual(2, Result.List[1].PlayerCount);
        }
    }
}