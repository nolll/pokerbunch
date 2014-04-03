using System.Collections.Generic;
using Core.Classes;
using Web.Models.PlayerModels.List;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IPlayerListPageModelFactory
    {
        PlayerListPageModel Create(Homegame homegame, IList<Player> players, bool isInManagerMode);
    }
}