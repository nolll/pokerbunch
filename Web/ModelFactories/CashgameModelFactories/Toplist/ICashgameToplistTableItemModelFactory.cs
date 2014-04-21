using Core.Classes;
using Core.UseCases.CashgameTopList;
using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public interface ICashgameToplistTableItemModelFactory
    {
        CashgameToplistTableItemModel Create(TopListItem toplistItem, string slug, CurrencySettings currency, ToplistSortOrder sortOrder);
    }
}