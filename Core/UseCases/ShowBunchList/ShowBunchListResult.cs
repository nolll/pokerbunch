using System.Collections.Generic;

namespace Core.UseCases.ShowBunchList
{
    public class ShowBunchListResult
    {
        public IList<BunchListItem> Bunches { get; set; }
    }
}