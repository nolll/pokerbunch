using Core.Classes;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public interface ICashgameListTableItemModelFactory
    {
        CashgameListTableItemModel Create(Homegame homegame, Cashgame cashgame, bool showYear, ListSortOrder sortOrder);
    }
}