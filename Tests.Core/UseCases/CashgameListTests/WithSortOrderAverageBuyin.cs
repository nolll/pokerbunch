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
            var result = Sut.Execute(Request);

            Assert.AreEqual(CashgameList.SortOrder.AverageBuyin, result.SortOrder);
            Assert.AreEqual(300, result.List[0].AverageBuyin.Amount);
            Assert.AreEqual(200, result.List[1].AverageBuyin.Amount);
        }
    }
}