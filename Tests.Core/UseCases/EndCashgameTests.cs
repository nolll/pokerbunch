using Core.UseCases.EndCashgame;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class EndCashgameTests : TestBase
    {
        [Test]
        public void EndGame_WithoutRunningGame_DoesNotEndGame()
        {
            var request = new EndCashgameRequest(Constants.SlugA);
            Sut.Execute(request);

            Assert.IsNull(Repos.Cashgame.Ended);
        }

        [Test]
        public void EndGame_WithRunningGame_EndsGame()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new EndCashgameRequest(Constants.SlugA);
            Sut.Execute(request);

            Assert.AreEqual(Constants.CashgameIdC, Repos.Cashgame.Ended.Id);
        }

        private EndCashgameInteractor Sut
        {
            get
            {
                return new EndCashgameInteractor(
                    Repos.Bunch,
                    Repos.Cashgame);
            }
        }
    }
}