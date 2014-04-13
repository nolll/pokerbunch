using System.Collections.Generic;

namespace Core.UseCases.BunchList
{
    public class BunchListResult
    {
        public IList<BunchListItem> Bunches { get; set; }
    }
}