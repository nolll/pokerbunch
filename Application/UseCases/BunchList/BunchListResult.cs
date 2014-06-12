using System.Collections.Generic;

namespace Application.UseCases.BunchList
{
    public class BunchListResult
    {
        public IList<BunchListItem> Bunches { get; private set; }

        public BunchListResult(IList<BunchListItem> bunches)
        {
            Bunches = bunches;
        }
    }
}