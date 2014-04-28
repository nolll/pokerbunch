using System.Collections.Generic;
using Application.UseCases.CashgameTopList;
using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public interface ICashgameToplistTableItemModelFactory
    {
        CashgameToplistTableItemModel Create(TopListItem toplistItem, string slug, ToplistSortOrder sortOrder);
        IList<CashgameToplistTableItemModel> CreateList(CashgameTopListResult topListResult);
    }
}