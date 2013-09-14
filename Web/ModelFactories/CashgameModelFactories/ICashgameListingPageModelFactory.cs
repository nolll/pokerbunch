using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Listing;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface ICashgameListingPageModelFactory
    {
        CashgameListingPageModel Create(User user, Homegame homegame, List<Cashgame> cashgames, List<int> years, int? year, Cashgame runningGame);
    }
}