using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public interface ICashgameListTableModelFactory
    {
        CashgameListTableModel Create(Homegame homegame, IList<Cashgame> cashgames, ListSortOrder sortOrder, int? year);
    }
}