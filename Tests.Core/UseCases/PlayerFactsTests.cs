using Core.UseCases.PlayerFacts;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class PlayerFactsTests : TestBase
    {
        [Test]
        public void PlayerFacts_ReturnsResultObject()
        {
            var request = new PlayerFactsRequest(TestData.SlugA, TestData.PlayerIdA);
            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.GamesPlayed);
            Assert.AreEqual(152, result.TimePlayed.Minutes);
            Assert.AreEqual(200, result.Winnings.Amount);
            Assert.AreEqual(350, result.BestResult.Amount);
            Assert.AreEqual(-150, result.WorstResult.Amount);
            Assert.AreEqual(1, result.BestResultCount);
            Assert.AreEqual(1, result.WinningStreak);
            Assert.AreEqual(1, result.LosingStreak);
        }

        private PlayerFactsInteractor Sut
        {
            get
            {
                return new PlayerFactsInteractor(
                    Repos.Bunch,
                    Repos.Cashgame);
            }
        }
    }
}