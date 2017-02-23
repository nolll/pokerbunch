using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithYearAndAnyPageButOverview : Arrange
    {
        protected override CashgameContext.CashgamePage SelectedPage => CashgameContext.CashgamePage.Unknown;
        protected override int? Year => FirstYear;

        [Test]
        public void YearIsSetToLatestYear()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(FirstYear, result.SelectedYear);
        }
    }
}