using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameListTests
{
    public class WithYear : Arrange
    {
        protected override int? Year => 2001;

        [Test]
        public void CashgameList_WithYear_YearIsSet()
        {
            Assert.IsTrue(Result.ShowYear);
            Assert.AreEqual(Year, Result.Year);
        }
    }
}