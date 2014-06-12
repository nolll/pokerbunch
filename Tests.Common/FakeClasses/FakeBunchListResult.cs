using System.Collections.Generic;
using Application.UseCases.BunchList;

namespace Tests.Common.FakeClasses
{
    public class FakeBunchListResult : BunchListResult
    {
        public FakeBunchListResult(
            IList<BunchListItem> bunches = null)
            : base(
                bunches ?? new List<BunchListItem>())
        {
        }
    }
}