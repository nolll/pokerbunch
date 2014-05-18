using Core.Entities;
using Web.Models.CashgameModels.Details;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public interface ICashgameDetailsTableItemModelFactory
    {
        CashgameDetailsTableItemModel Create(Homegame homegame, Cashgame cashgame, Player player, CashgameResult result);
    }
}