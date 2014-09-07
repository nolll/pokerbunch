﻿using System.Collections.Generic;
using Application.UseCases.PlayerFacts;
using Core.Entities;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class PlayerFactsTests : MockContainer
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
            var homegame = new BunchInTest();

            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<ICashgameRepository>().Setup(o => o.GetPublished(homegame, null)).Returns(GetCashgames());

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
                        new CashgameResultInTest(SecondPlayerId)
                    };
            var cashgame1 = new CashgameInTest(results: results1);

            var results2 = new List<CashgameResult>
                    {
                        new CashgameResultInTest(SelectedPlayerId, winnings: -2, playedTime: 2),
                        new CashgameResultInTest(SecondPlayerId),
                    };
            var cashgame2 = new CashgameInTest(results: results2);

            var results3 = new List<CashgameResult>
                    {
                        new CashgameResultInTest(SelectedPlayerId, winnings: 3, playedTime: 2),
                        new CashgameResultInTest(SecondPlayerId),
                    };
            var cashgame3 = new CashgameInTest(results: results3);

            var results4 = new List<CashgameResult>
                    {
                        new CashgameResultInTest(SelectedPlayerId, winnings: 4, playedTime: 2),
                        new CashgameResultInTest(SecondPlayerId),
                    };
            var cashgame4 = new CashgameInTest(results: results4);

            var results5 = new List<CashgameResult>
                    {
                        new CashgameResultInTest(SecondPlayerId),
                        new CashgameResultInTest(ThirdPlayerId)
                    };
            var cashgame5 = new CashgameInTest(results: results5);

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