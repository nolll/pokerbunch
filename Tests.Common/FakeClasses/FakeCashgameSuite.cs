using System.Collections.Generic;
using Core.Classes;

namespace Tests.Common.FakeClasses
{
    public class FakeCashgameSuite : CashgameSuite
    {
        public FakeCashgameSuite(
            IList<Cashgame> cashgames = default(IList<Cashgame>),
            IList<CashgameTotalResult> totalResults = default(IList<CashgameTotalResult>),
            IList<Player> players = default(IList<Player>) 
            ) : 
            base(
            cashgames ?? new List<Cashgame>(),
            totalResults ?? new List<CashgameTotalResult>(),
            players ?? new List<Player>())
        {
        }
    }
}