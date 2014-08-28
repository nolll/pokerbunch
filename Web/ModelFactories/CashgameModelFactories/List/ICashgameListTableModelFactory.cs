using System.Collections.Generic;
using Core.Entities;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public interface ICashgameListTableModelFactory
    {
        CashgameListTableModel Create(Bunch bunch, IList<Cashgame> cashgames, ListSortOrder sortOrder, int? year);
    }
}