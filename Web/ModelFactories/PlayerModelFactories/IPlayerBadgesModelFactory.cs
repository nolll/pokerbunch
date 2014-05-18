using System.Collections.Generic;
using Core.Entities;
using Web.Models.PlayerModels.Badges;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IPlayerBadgesModelFactory
    {
        PlayerBadgesModel Create(int playerId, IList<Cashgame> cashgames);
    }
}