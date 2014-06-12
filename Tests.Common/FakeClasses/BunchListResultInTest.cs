using System.Collections.Generic;
using Application.UseCases.BunchList;

namespace Tests.Common.FakeClasses
{
    public class BunchListResultInTest : BunchListResult
    {
        public BunchListResultInTest(
            IList<BunchListItem> bunches = null)
            : base(
                bunches ?? new List<BunchListItem>())
        {
        }
    }
}