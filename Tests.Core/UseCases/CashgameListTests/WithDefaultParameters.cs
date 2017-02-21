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
            var result = Sut.Execute(Request);

            Assert.AreEqual(CashgameList.SortOrder.Date, result.SortOrder);
            Assert.AreEqual("2001-01-02", result.List[0].Date.IsoString);
            Assert.AreEqual("2001-01-01", result.List[1].Date.IsoString);
        }

        [Test]
        public void FirstItemLocationIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(LocationData.Name2, result.List[0].Location);
        }

        [Test]
        public void FirstItemUrlIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(CashgameData.Id2, result.List[0].CashgameId);
        }

        [Test]
        public void FirstItemDurationIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(122, result.List[0].Duration.Minutes);
        }

        [Test]
        public void FirstItemPlayerCountIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(2, result.List[0].PlayerCount);
        }

        [Test]
        public void FirstItemTurnoverIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(400, result.List[0].Turnover.Amount);
        }

        [Test]
        public void FirstItemAverageBuyinIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(200, result.List[0].AverageBuyin.Amount);
        }

        [Test]
        public void SlugIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(BunchData.Id1, result.Slug);
        }

        [Test]
        public void YearIsNull()
        {
            var result = Sut.Execute(Request);

            Assert.IsFalse(result.ShowYear);
            Assert.IsNull(result.Year);
        }
    }
}