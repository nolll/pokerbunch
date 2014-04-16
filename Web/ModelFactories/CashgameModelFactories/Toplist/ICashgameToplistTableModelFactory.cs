using Core.Classes;
using Core.UseCases.CashgameTopList;
using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public interface ICashgameToplistTableModelFactory
    {
        CashgameToplistTableModel Create(Homegame homegame, CashgameSuite suite, int? year, ToplistSortOrder sortOrder);
        CashgameToplistTableModel Create(CashgameTopListResult topListResult);
    }
}