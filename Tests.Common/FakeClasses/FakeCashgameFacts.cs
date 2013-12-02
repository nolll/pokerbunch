using Core.Classes;

namespace Tests.Common.FakeClasses
{
    public class FakeCashgameFacts : CashgameFacts
    {
        public FakeCashgameFacts(
            int gameCount = default(int),
            CashgameResult bestResult = default(CashgameResult),
            CashgameResult worstResult = default(CashgameResult),
            CashgameTotalResult bestTotalResult = default(CashgameTotalResult),
            CashgameTotalResult worstTotalResult = default(CashgameTotalResult),
            CashgameTotalResult mostTimeResult = default(CashgameTotalResult),
            CashgameTotalResult biggestBuyinTotalResult = default(CashgameTotalResult),
            CashgameTotalResult biggestCashoutTotalResult = default(CashgameTotalResult),
            int totalGameTime = default(int),
            int totalTurnover = default(int)) : 
                base(
                gameCount,
                bestResult,
                worstResult,
                bestTotalResult,
                worstTotalResult,
                mostTimeResult,
                biggestBuyinTotalResult,
                biggestCashoutTotalResult,
                totalGameTime,
                totalTurnover)
        {
        }
    }
}