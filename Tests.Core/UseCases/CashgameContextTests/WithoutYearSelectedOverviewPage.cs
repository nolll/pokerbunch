using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithoutYearSelectedOverviewPage : Arrange
    {
        protected override CashgameContext.CashgamePage SelectedPage => CashgameContext.CashgamePage.Overview;

        [Test]
        public void Execute_WithoutYear_SelectedYearIsNull()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(LastYear, result.SelectedYear);
        }
    }
}