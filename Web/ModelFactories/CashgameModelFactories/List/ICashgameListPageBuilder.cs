using System.Collections.Generic;
using Core.Entities;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public interface ICashgameListPageBuilder
    {
        CashgameListPageModel Build(Homegame homegame, IList<Cashgame> cashgames, IList<int> years, ListSortOrder sortOrder, int? year);
    }
}