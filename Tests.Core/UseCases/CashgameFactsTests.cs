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
            var request = new CashgameFactsRequest(Constants.SlugA, null);
            var result = Execute2(request);

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

        private CashgameFactsResult GetFactsResult(Bunch bunch, FactBuilderInTest factBuilder)
        {
            return CashgameFactsInteractor.GetFactsResult(GetMock<IPlayerRepository>().Object, bunch, factBuilder);
        }

        private CashgameFactsResult Execute(CashgameFactsRequest request)
        {
            return CashgameFactsInteractor.Execute(
                Repos.Bunch,
                GetMock<ICashgameRepository>().Object,
                Repos.Player,
                request);
        }

        private CashgameFactsResult Execute2(CashgameFactsRequest request)
        {
            return CashgameFactsInteractor.Execute(
                Repos.Bunch,
                Repos.Cashgame,
                Repos.Player,
                request);
        }
    }
}
