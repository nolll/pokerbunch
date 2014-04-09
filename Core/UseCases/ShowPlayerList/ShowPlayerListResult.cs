using System.Collections.Generic;
using Tests.Core.UseCases;

namespace Core.UseCases.ShowPlayerList
{
    public class ShowPlayerListResult
    {
        public IList<PlayerListItem> Players;
        public string Slug;
    }
}