using Core.UseCases;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameListTests
{
    public class WithDefaultParameters : Arrange
    {
        [Test]
        public void LatestGameIsFirst()
        {
            Assert.AreEqual(CashgameList.SortOrder.Date, Result.SortOrder);
            Assert.AreEqual("2001-01-02", Result.List[0].Date.IsoString);
            Assert.AreEqual("2001-01-01", Result.List[1].Date.IsoString);
        }

        [Test]
        public void FirstItemLocationIsSet() => Assert.AreEqual(LocationData.Name2, Result.List[0].Location);

        [Test]
        public void FirstItemUrlIsSet() => Assert.AreEqual(CashgameData.Id2, Result.List[0].CashgameId);

        [Test]
        public void FirstItemDurationIsSet() => Assert.AreEqual(122, Result.List[0].Duration.Minutes);

        [Test]
        public void FirstItemPlayerCountIsSet() => Assert.AreEqual(2, Result.List[0].PlayerCount);

        [Test]
        public void FirstItemTurnoverIsSet() => Assert.AreEqual(400, Result.List[0].Turnover.Amount);

        [Test]
        public void FirstItemAverageBuyinIsSet() => Assert.AreEqual(200, Result.List[0].AverageBuyin.Amount);

        [Test]
        public void SlugIsSet() => Assert.AreEqual(BunchData.Id1, Result.Slug);

        [Test]
        public void YearIsNull()
        {
            Assert.IsFalse(Result.ShowYear);
            Assert.IsNull(Result.Year);
        }
    }
}