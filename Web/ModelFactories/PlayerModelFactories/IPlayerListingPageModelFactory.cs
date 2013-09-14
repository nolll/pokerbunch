using System.Collections.Generic;
using Core.Classes;
using Web.Models.PlayerModels.Listing;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IPlayerListingPageModelFactory
    {
        PlayerListingPageModel Create(User user, Homegame homegame, List<Player> players, bool isInManagerMode, Cashgame runningGame);
    }
}