using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases.PlayerFacts;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Core.UseCases
{
    class PlayerFactsTests : TestBase
    {
        private const int SelectedPlayerId = 1;
        private const int SecondPlayerId = 2;
        private const int ThirdPlayerId = 3;

        [Test]
        public void PlayerFacts_ReturnsResultObject()
        {
            const string slug = "a";
            const int playerId = 1;
            var request = new PlayerFactsRequest(slug, playerId);
            var bunch = A.Bunch.Build();

            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(slug)).Returns(bunch);
            GetMock<ICashgameRepository>().Setup(o => o.GetFinished(It.IsAny<int>(), null)).Returns(GetCashgames());

            var result = Execute(request);

            Assert.AreEqual(4, result.GamesPlayed);
            Assert.AreEqual(8, result.TimePlayed.Minutes);
            Assert.AreEqual(4, result.Winnings.Amount);
            Assert.AreEqual(4, result.BestResult.Amount);
            Assert.AreEqual(-2, result.WorstResult.Amount);
            Assert.AreEqual(2, result.BestResultCount);
            Assert.AreEqual(2, result.WinningStreak);
            Assert.AreEqual(2, result.LosingStreak);
        }

        private IList<Cashgame> GetCashgames()
        {
            var results1 = new List<CashgameResult>
                    {
                        new CashgameResultInTest(SelectedPlayerId, winnings: -1, playedTime: 2),
                        A.CashgameResult.WithPlayerId(SecondPlayerId).Build()
                    };
            var cashgame1 = A.Cashgame.WithResults(results1).Build();

            var results2 = new List<CashgameResult>
                    {
                        new CashgameResultInTest(SelectedPlayerId, winnings: -2, playedTime: 2),
                        A.CashgameResult.WithPlayerId(SecondPlayerId).Build(),
                    };
            var cashgame2 = A.Cashgame.WithResults(results2).Build();

            var results3 = new List<CashgameResult>
                    {
                        new CashgameResultInTest(SelectedPlayerId, winnings: 3, playedTime: 2),
                        A.CashgameResult.WithPlayerId(SecondPlayerId).Build(),
                    };
            var cashgame3 = A.Cashgame.WithResults(results3).Build();

            var results4 = new List<CashgameResult>
                    {
                        new CashgameResultInTest(SelectedPlayerId, winnings: 4, playedTime: 2),
                        A.CashgameResult.WithPlayerId(SecondPlayerId).Build(),
                    };
            var cashgame4 = A.Cashgame.WithResults(results4).Build();

            var results5 = new List<CashgameResult>
                    {
                        A.CashgameResult.WithPlayerId(SecondPlayerId).Build(),
                        A.CashgameResult.WithPlayerId(ThirdPlayerId).Build()
                    };
            var cashgame5 = A.Cashgame.WithResults(results5).Build();

            return new List<Cashgame> { cashgame1, cashgame2, cashgame3, cashgame4, cashgame5 };
        }

        private PlayerFactsResult Execute(PlayerFactsRequest request)
        {
            return PlayerFactsInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                request);
        }
    }
}