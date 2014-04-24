using Application.UseCases.CashgameTopList;
using Core.Classes;
using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public interface ICashgameToplistTableItemModelFactory
    {
        CashgameToplistTableItemModel Create(TopListItem toplistItem, string slug, CurrencySettings currency, ToplistSortOrder sortOrder);
    }
}