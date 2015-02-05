using Core.Exceptions;
using Core.UseCases.DeleteCashgame;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class DeleteCashgameTests : TestBase
    {
        [Test]
        public void DeleteCashgame_GameHasResults_ThrowsCashgameHasResultsException()
        {
            var request = new DeleteCashgameRequest(Constants.SlugA, Constants.CashgameIdA);

            Assert.Throws<CashgameHasResultsException>(() => Sut.Execute(request));
        }

        [Test]
        public void DeleteCashgame_GameHasNoResults_DeletesGame()
        {
            Repos.Cashgame.SetupEmptyGame();

            var request = new DeleteCashgameRequest(Constants.SlugA, Constants.CashgameIdA);

            Sut.Execute(request);

            Assert.AreEqual(Constants.CashgameIdA, Repos.Cashgame.Deleted.Id);
        }

        [Test]
        public void DeleteCashgame_GameHasNoResults_ReturnUrlIsSet()
        {
            Repos.Cashgame.SetupEmptyGame();

            var request = new DeleteCashgameRequest(Constants.SlugA, Constants.CashgameIdA);

            var result = Sut.Execute(request);

            Assert.AreEqual("/bunch-a/cashgame/index", result.ReturnUrl.Relative);
        }

        private DeleteCashgameInteractor Sut
        {
            get
            {
                return new DeleteCashgameInteractor(
                    Repos.Cashgame);
            }
        }
    }
}