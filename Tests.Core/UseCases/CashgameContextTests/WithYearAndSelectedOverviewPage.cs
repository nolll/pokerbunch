using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithYearAndSelectedOverviewPage : Arrange
    {
        protected override int? Year => FirstYear;

        [Test]
        public void YearIsSetToLatestYear()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(LastYear, result.SelectedYear);
        }
    }
}