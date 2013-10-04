using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Details;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface ICashgameDetailsPageModelFactory
    {
        CashgameDetailsPageModel Create(User user, Homegame homegame, Cashgame cashgame, Player player, IList<int> years, bool isManager, Cashgame runningGame = null);
    }
}