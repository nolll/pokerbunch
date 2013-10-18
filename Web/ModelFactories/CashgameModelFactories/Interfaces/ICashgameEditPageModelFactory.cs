using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Edit;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface ICashgameEditPageModelFactory
    {
        CashgameEditPageModel Create(User user, Homegame homegame, Cashgame cashgame, IList<string> locations, IList<int> years, Cashgame runningGame);
        CashgameEditPageModel Create(User user, Homegame homegame, Cashgame cashgame, IList<string> locations, IList<int> years, Cashgame runningGame, CashgameEditPostModel postModel);
    }
}