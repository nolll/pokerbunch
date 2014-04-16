using Core.Classes;
using Core.UseCases.CashgameTopList;
using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public interface ICashgameToplistTableItemModelFactory
    {
        CashgameToplistTableItemModel Create(Homegame homegame, Player player, CashgameTotalResult result, int rank, ToplistSortOrder sortOrder);
        CashgameToplistTableItemModel Create(TopListItem toplistItem, string slug, CurrencySettings currency, ToplistSortOrder sortOrder);
    }
}