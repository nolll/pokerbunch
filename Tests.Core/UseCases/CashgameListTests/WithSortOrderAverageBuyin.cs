using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameListTests
{
    public class WithSortOrderAverageBuyin : Arrange
    {
        protected override CashgameList.SortOrder SortOrder => CashgameList.SortOrder.AverageBuyin;

        [Test]
        public void HighestAverageBuyinIsFirst()
        {
            Assert.AreEqual(CashgameList.SortOrder.AverageBuyin, Result.SortOrder);
            Assert.AreEqual(300, Result.List[0].AverageBuyin.Amount);
            Assert.AreEqual(200, Result.List[1].AverageBuyin.Amount);
        }
    }
}