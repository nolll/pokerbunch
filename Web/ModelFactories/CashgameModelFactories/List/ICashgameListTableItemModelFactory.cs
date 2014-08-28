using Core.Entities;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public interface ICashgameListTableItemModelFactory
    {
        CashgameListTableItemModel Create(Bunch bunch, Cashgame cashgame, bool showYear, ListSortOrder sortOrder);
    }
}