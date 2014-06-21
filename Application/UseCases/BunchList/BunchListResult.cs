using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Application.UseCases.BunchList
{
    public class BunchListResult
    {
        public IList<BunchListItem> Bunches { get; private set; }

        public BunchListResult(IEnumerable<Homegame> homegames)
        {
            Bunches = homegames.Select(o => new BunchListItem(o)).ToList();
        }

        protected BunchListResult(IList<BunchListItem> bunches)
        {
            Bunches = bunches;
        }
    }
}