using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Core.UseCases.BunchList
{
    public class BunchListResult
    {
        public IList<BunchListItem> Bunches { get; private set; }

        public BunchListResult(IEnumerable<Bunch> homegames)
        {
            Bunches = homegames.Select(o => new BunchListItem(o)).ToList();
        }
    }
}