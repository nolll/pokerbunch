using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases.CashgameFacts;
using Moq;
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
            const int year = 1;
            var player1 = A.Player.Build();
            var player2 = A.Player.Build();
            var players = new List<Player>{player1, player2};
            var cashgame1BestResult = A.CashgameResult.Build();
            var cashgame1WorstResult = A.CashgameResult.Build();
            var cashgame1Results = new List<CashgameResult> {cashgame1BestResult, cashgame1WorstResult};
            var cashgame1 = A.Cashgame.WithResults(cashgame1Results).Build();
            var cashgame2BestResult = A.CashgameResult.Build();
            var cashgame2WorstResult = A.CashgameResult.Build();
            var cashgame2Results = new List<CashgameResult> { cashgame2BestResult, cashgame2WorstResult };
            var cashgame2 = A.Cashgame.WithResults(cashgame2Results).Build();
            var cashgames = new List<Cashgame>{cashgame1, cashgame2};

            GetMock<IPlayerRepository>().Setup(o => o.GetList(It.IsAny<int>())).Returns(players);
            GetMock<ICashgameRepository>().Setup(o => o.GetFinished(It.IsAny<int>(), year)).Returns(cashgames);

            var request = new CashgameFactsRequest(Constants.SlugA, year);
            var result = Execute(request);

            Assert.IsNotNull(result);
        }

        // todo: 
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

        private CashgameFactsResult GetFactsResult(Bunch bunch, FactBuilderInTest factBuilder)
        {
            return CashgameFactsInteractor.GetFactsResult(GetMock<IPlayerRepository>().Object, bunch, factBuilder);
        }
        
        private CashgameFactsResult Execute(CashgameFactsRequest request)
        {
            return CashgameFactsInteractor.Execute(
                Repo.Bunch,
                GetMock<ICashgameRepository>().Object,
                GetMock<IPlayerRepository>().Object,
                request);
        }
    }
}
