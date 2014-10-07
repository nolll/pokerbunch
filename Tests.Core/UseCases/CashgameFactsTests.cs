using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases.CashgameFacts;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Core.UseCases
{
    class CashgameFactsTests : TestBase
    {
        [Test]
        public void Execute_ReturnsCashgameFactResult()
        {
            const string slug = "a";
            const int year = 1;
            var bunch = A.Bunch.Build();
            var player1 = new PlayerInTest();
            var player2 = new PlayerInTest();
            var players = new List<Player>{player1, player2};
            var cashgame1BestResult = new CashgameResultInTest();
            var cashgame1WorstResult = new CashgameResultInTest();
            var cashgame1Results = new List<CashgameResult> {cashgame1BestResult, cashgame1WorstResult};
            var cashgame1 = new CashgameInTest(results:cashgame1Results);
            var cashgame2BestResult = new CashgameResultInTest();
            var cashgame2WorstResult = new CashgameResultInTest();
            var cashgame2Results = new List<CashgameResult> { cashgame2BestResult, cashgame2WorstResult };
            var cashgame2 = new CashgameInTest(results:cashgame2Results);
            var cashgames = new List<Cashgame>{cashgame1, cashgame2};

            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(slug)).Returns(bunch);
            GetMock<IPlayerRepository>().Setup(o => o.GetList(bunch)).Returns(players);
            GetMock<ICashgameRepository>().Setup(o => o.GetPublished(bunch, year)).Returns(cashgames);

            var request = new CashgameFactsRequest(slug, year);
            var result = Execute(request);

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetFactsResult_AllPropertiesAreSet()
        {
            var bunch = A.Bunch.Build();
            var factBuilder = new FactBuilderInTest(
                gameCount: 2,
                totalGameTime: 3,
                totalTurnover: 4,
                worstResult: new CashgameResultInTest(winnings: 5),
                bestResult: new CashgameResultInTest(winnings: 6),
                worstTotalResult: new CashgameTotalResultInTest(winnings: 7),
                bestTotalResult: new CashgameTotalResultInTest(winnings: 8),
                biggestBuyinTotalResult: new CashgameTotalResultInTest(buyin: 9),
                biggestCashoutTotalResult: new CashgameTotalResultInTest(cashout: 10),
                mostTimeResult: new CashgameTotalResultInTest(timePlayed: 11));

            var result = GetFactsResult(bunch, factBuilder);

            Assert.AreEqual(2, result.GameCount);
            Assert.AreEqual(3, result.TotalTimePlayed.Minutes);
            Assert.AreEqual(4, result.Turnover.Amount);
            Assert.AreEqual(5, result.WorstResult.Amount.Amount);
            Assert.AreEqual(6, result.BestResult.Amount.Amount);
            Assert.AreEqual(7, result.WorstTotalResult.Amount.Amount);
            Assert.AreEqual(8, result.BestTotalResult.Amount.Amount);
            Assert.AreEqual(9, result.BiggestBuyin.Amount.Amount);
            Assert.AreEqual(10, result.BiggestCashout.Amount.Amount);
            Assert.AreEqual(11, result.MostTimePlayed.Time.Minutes);
        }

        private CashgameFactsResult GetFactsResult(Bunch homegame, FactBuilderInTest factBuilder)
        {
            return CashgameFactsInteractor.GetFactsResult(GetMock<IPlayerRepository>().Object, homegame, factBuilder);
        }
        
        private CashgameFactsResult Execute(CashgameFactsRequest request)
        {
            return CashgameFactsInteractor.Execute(GetMock<IBunchRepository>().Object, GetMock<ICashgameRepository>().Object, GetMock<IPlayerRepository>().Object, request);
        }
    }
}
