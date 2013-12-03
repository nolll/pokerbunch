using Core.Classes;
using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public interface ICashgameToplistTableModelFactory
    {
        CashgameToplistTableModel Create(Homegame homegame, CashgameSuite suite, int? year, ToplistSortOrder sortOrder);
    }
}