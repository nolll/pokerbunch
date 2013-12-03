using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public interface ICashgameListPageModelFactory
    {
        CashgameListPageModel Create(User user, Homegame homegame, IList<Cashgame> cashgames, IList<int> years, int? year);
    }
}