using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithSelectedChartPage : Arrange
    {
        protected override CashgameContext.CashgamePage SelectedPage => CashgameContext.CashgamePage.Chart;

        [Test]
        public void SelectedPageIsCorrect() => Assert.AreEqual(SelectedPage, Result.SelectedPage);
    }
}