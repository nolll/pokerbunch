using Core.Classes;
using Web.Models.CashgameModels.Details;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface ICashgameDetailsTableModelFactory
    {
        CashgameDetailsTableModel Create(Homegame homegame, Cashgame cashgame);
    }
}