using Core.Entities;
using Core.UseCases.CashgameFacts;
using Tests.Common.Builders;

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
                bestResult ?? new CashgameResultBuilder().Build(),
                worstResult ?? new CashgameResultBuilder().Build(),
                bestTotalResult ?? new CashgameTotalResultBuilder().Build(),
                worstTotalResult ?? new CashgameTotalResultBuilder().Build(),
                mostTimeResult ?? new CashgameTotalResultBuilder().Build(),
                biggestBuyinTotalResult ?? new CashgameTotalResultBuilder().Build(),
                biggestCashoutTotalResult ?? new CashgameTotalResultBuilder().Build(),
                totalGameTime,
                totalTurnover)
        {
        }
    }
}