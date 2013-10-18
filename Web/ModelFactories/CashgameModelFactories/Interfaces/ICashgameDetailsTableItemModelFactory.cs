using Core.Classes;
using Web.Models.CashgameModels.Details;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface ICashgameDetailsTableItemModelFactory
    {
        CashgameDetailsTableItemModel Create(Homegame homegame, Cashgame cashgame, CashgameResult result);
    }
}