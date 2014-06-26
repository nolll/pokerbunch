using System.Collections.Generic;
using Application.UseCases.PlayerFacts;
using Core.Entities;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class PlayerFactsInteractorTests : MockContainer
    {
        [Test]
        public void Execute_ReturnsResultObject()
        {
            const string slug = "a";
            const int playerId = 1;
            var request = new PlayerFactsRequest(slug, playerId);
            var homegame = new HomegameInTest();
            var cashgames = new List<Cashgame>();

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<ICashgameRepository>().Setup(o => o.GetPublished(homegame, null)).Returns(cashgames);

            var result = Sut.Execute(request);

            Assert.IsInstanceOf<PlayerFactsResult>(result);
        }

        private PlayerFactsInteractor Sut
        {
            get
            {
                return new PlayerFactsInteractor(
                    GetMock<IHomegameRepository>().Object,
                    GetMock<ICashgameRepository>().Object);
            }
        }
    }

    class PlayerFactsResultTests
    {
        [Test]
        public void Construct_AllPropertiesAreSet()
        {
            var cashgames = new List<Cashgame>();
            const int playerId = 1;
            var currency = Currency.Default;

            var result = new PlayerFactsResult(cashgames, playerId, currency);
            Assert.AreEqual(0, result.GamesPlayed);
            Assert.AreEqual(0, result.TimePlayed.Minutes);
            Assert.AreEqual(0, result.Winnings.Amount);
            Assert.AreEqual(0, result.BestResult.Amount);
            Assert.AreEqual(0, result.WorstResult.Amount);
            Assert.AreEqual(0, result.BestResultCount);
            Assert.AreEqual(0, result.WinningStreak);
            Assert.AreEqual(0, result.LosingStreak);
        }
    }
}