using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameListTests
{
    public class WithYear : Arrange
    {
        protected override int? Year => 2001;

        [Test]
        public void CashgameList_WithYear_YearIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.IsTrue(result.ShowYear);
            Assert.AreEqual(Year, result.Year);
        }
    }
}