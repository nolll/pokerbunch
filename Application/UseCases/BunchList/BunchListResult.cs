using System.Collections.Generic;

namespace Application.UseCases.BunchList
{
    public class BunchListResult
    {
        public IList<BunchListItem> Bunches { get; set; }
    }
}