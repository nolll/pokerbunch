using System.Collections.Generic;
using Core.Classes;

namespace Tests.Common.FakeClasses
{
    public class FakeCashgameSuite : CashgameSuite
    {
        public FakeCashgameSuite(
            IList<Cashgame> cashgames = default(IList<Cashgame>),
            IList<CashgameTotalResult> totalResults = default(IList<CashgameTotalResult>), 
            int gameCount = default(int), 
            CashgameTotalResult bestTotalResult = default(CashgameTotalResult), 
            CashgameResult bestResult = default(CashgameResult), 
            CashgameResult worstResult = default(CashgameResult), 
            CashgameTotalResult mostTimeResult = default(CashgameTotalResult), 
            int totalGameTime = default(int)) : 
                base(
                cashgames ?? new List<Cashgame>(),
                totalResults ?? new List<CashgameTotalResult>(), 
                gameCount, 
                bestTotalResult, 
                bestResult, 
                worstResult, 
                mostTimeResult, 
                totalGameTime)
        {
        }
    }
}