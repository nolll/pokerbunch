using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithSelectedFactsPage : Arrange
    {
        protected override CashgameContext.CashgamePage SelectedPage => CashgameContext.CashgamePage.Facts;

        [Test]
        public void SelectedPageIsCorrect() => Assert.AreEqual(SelectedPage, Result.SelectedPage);
    }
}