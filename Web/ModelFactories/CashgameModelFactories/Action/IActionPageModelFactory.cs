using Core.Classes;
using Web.Models.CashgameModels.Action;

namespace Web.ModelFactories.CashgameModelFactories.Action
{
    public interface IActionPageModelFactory
    {
        ActionPageModel Create(Homegame homegame, Cashgame cashgame, Player player, CashgameResult result, Role role);
    }
}