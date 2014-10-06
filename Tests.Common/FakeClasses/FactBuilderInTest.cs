using Core.Entities;
using Core.UseCases.CashgameFacts;

namespace Tests.Common.FakeClasses
{
    public class FactBuilderInTest : FactBuilder
    {
        public FactBuilderInTest(
            int gameCount = 0,
            CashgameResult bestResult = null,
            CashgameResult worstResult = null,
            CashgameTotalResult bestTotalResult = null,
            CashgameTotalResult worstTotalResult = null,
            CashgameTotalResult mostTimeResult = null,
            CashgameTotalResult biggestBuyinTotalResult = null,
            CashgameTotalResult biggestCashoutTotalResult = null,
            int totalGameTime = 0,
            int totalTurnover = 0) : 
                base(
                gameCount,
                bestResult ?? new CashgameResultInTest(),
                worstResult ?? new CashgameResultInTest(),
                bestTotalResult ?? new CashgameTotalResultInTest(),
                worstTotalResult ?? new CashgameTotalResultInTest(),
                mostTimeResult ?? new CashgameTotalResultInTest(),
                biggestBuyinTotalResult ?? new CashgameTotalResultInTest(),
                biggestCashoutTotalResult ?? new CashgameTotalResultInTest(),
                totalGameTime,
                totalTurnover)
        {
        }
    }
}