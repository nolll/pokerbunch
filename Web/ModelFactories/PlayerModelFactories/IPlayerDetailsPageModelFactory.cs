using System.Collections.Generic;
using Core.Entities;
using Web.Models.PlayerModels.Details;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IPlayerDetailsPageModelFactory
    {
        PlayerDetailsPageModel Create(Homegame homegame, Player player, User user, IList<Cashgame> cashgames, bool isManager, bool hasPlayed);
    }
}