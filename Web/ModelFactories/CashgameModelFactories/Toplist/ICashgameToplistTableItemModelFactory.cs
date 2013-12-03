using Core.Classes;
using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public interface ICashgameToplistTableItemModelFactory
    {
        CashgameToplistTableItemModel Create(Homegame homegame, Player player, CashgameTotalResult result, int rank, ToplistSortOrder sortOrder);
    }
}