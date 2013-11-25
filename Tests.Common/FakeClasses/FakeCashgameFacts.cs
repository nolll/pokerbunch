using Core.Classes;

namespace Tests.Common.FakeClasses
{
    public class FakeCashgameFacts : CashgameFacts
    {
        public FakeCashgameFacts(
            int gameCount = default(int), 
            CashgameTotalResult bestTotalResult = default(CashgameTotalResult), 
            CashgameResult bestResult = default(CashgameResult), 
            CashgameResult worstResult = default(CashgameResult), 
            CashgameTotalResult mostTimeResult = default(CashgameTotalResult),
            int totalGameTime = default(int),
            int totalTurnover = default(int)) : 
                base(
                gameCount, 
                bestTotalResult, 
                bestResult, 
                worstResult, 
                mostTimeResult, 
                totalGameTime,
                totalTurnover)
        {
        }
    }
}