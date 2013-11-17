using System.Collections.Generic;
using Core.Classes;
using Web.Models.PlayerModels.Details;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IPlayerDetailsPageModelFactory
    {
        PlayerDetailsPageModel Create(User currentUser, Homegame homegame, Player player, User user, IList<Cashgame> cashgames, bool isManager, bool hasPlayed);
    }
}