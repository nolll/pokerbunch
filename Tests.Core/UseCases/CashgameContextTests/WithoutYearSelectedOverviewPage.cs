using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithoutYearSelectedOverviewPage : Arrange
    {
        protected override CashgameContext.CashgamePage SelectedPage => CashgameContext.CashgamePage.Overview;

        [Test]
        public void SelectedYearIsNull() => Assert.AreEqual(LastYear, Result.SelectedYear);
    }
}