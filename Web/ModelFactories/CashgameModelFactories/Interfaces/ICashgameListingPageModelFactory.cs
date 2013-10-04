using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Listing;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface ICashgameListingPageModelFactory
    {
        CashgameListingPageModel Create(User user, Homegame homegame, IList<Cashgame> cashgames, IList<int> years, int? year, Cashgame runningGame);
    }
}