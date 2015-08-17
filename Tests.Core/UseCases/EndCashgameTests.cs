using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class EndCashgameTests : TestBase
    {
        [Test]
        public void EndGame_WithoutRunningGame_DoesNotEndGame()
        {
            var request = new EndCashgame.Request(TestData.UserNameA, TestData.SlugA);
            Sut.Execute(request);

            Assert.IsNull(Repos.Cashgame.Ended);
        }

        [Test]
        public void EndGame_WithRunningGame_EndsGame()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new EndCashgame.Request(TestData.UserNameA, TestData.SlugA);
            Sut.Execute(request);

            Assert.AreEqual(TestData.CashgameIdC, Repos.Cashgame.Ended.Id);
        }

        private EndCashgame Sut
        {
            get
            {
                return new EndCashgame(
                    Repos.Bunch,
                    Repos.Cashgame,
                    Repos.User,
                    Repos.Player);
            }
        }
    }
}