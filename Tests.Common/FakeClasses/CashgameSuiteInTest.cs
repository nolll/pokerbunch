using System.Collections.Generic;
using Core.Entities;

namespace Tests.Common.FakeClasses
{
    public class CashgameSuiteInTest : CashgameSuite
    {
        public CashgameSuiteInTest(
            IList<Cashgame> cashgames = default(IList<Cashgame>),
            IList<CashgameTotalResult> totalResults = default(IList<CashgameTotalResult>))
            : base(
            cashgames ?? new List<Cashgame>(),
            totalResults ?? new List<CashgameTotalResult>())
        {
        }
    }
}