using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Action;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface IActionPageModelFactory
    {
        ActionPageModel Create(User user, Homegame homegame, Cashgame cashgame, Player player, CashgameResult result, Role role, IList<int> years = null, Cashgame runningGame = null);
    }
}