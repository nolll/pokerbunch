using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithYearAndSelectedOverviewPage : Arrange
    {
        protected override int? Year => FirstYear;

        [Test]
        public void YearIsSetToLatestYear() => Assert.AreEqual(LastYear, Result.SelectedYear);
    }
}