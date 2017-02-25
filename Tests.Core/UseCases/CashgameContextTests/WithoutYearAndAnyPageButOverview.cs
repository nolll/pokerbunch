using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithoutYearAndAnyPageButOverview : Arrange
    {
        protected override CashgameContext.CashgamePage SelectedPage => CashgameContext.CashgamePage.Unknown;

        [Test]
        public void SelectedYearIsNull() => Assert.IsNull(Result.SelectedYear);
    }
}