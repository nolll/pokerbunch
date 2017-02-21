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
            var result = Sut.Execute(Request);

            Assert.AreEqual(CashgameList.SortOrder.PlayerCount, result.SortOrder);
            Assert.AreEqual(2, result.List[0].PlayerCount);
            Assert.AreEqual(2, result.List[1].PlayerCount);
        }
    }
}