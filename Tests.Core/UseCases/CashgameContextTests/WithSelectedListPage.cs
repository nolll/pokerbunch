using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithSelectedListPage : Arrange
    {
        protected override CashgameContext.CashgamePage SelectedPage => CashgameContext.CashgamePage.List;

        [Test]
        public void SelectedPageIsCorrect() => Assert.AreEqual(SelectedPage, Result.SelectedPage);
    }
}