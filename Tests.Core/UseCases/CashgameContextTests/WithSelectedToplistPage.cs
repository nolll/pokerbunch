using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithSelectedToplistPage : Arrange
    {
        protected override CashgameContext.CashgamePage SelectedPage => CashgameContext.CashgamePage.Toplist;

        [Test]
        public void SelectedPageIsCorrect() => Assert.AreEqual(SelectedPage, Result.SelectedPage);
    }
}