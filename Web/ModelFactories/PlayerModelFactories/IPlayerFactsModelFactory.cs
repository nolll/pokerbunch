using System.Collections.Generic;
using Core.Classes;
using Web.Models.PlayerModels.Facts;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IPlayerFactsModelFactory
    {
        PlayerFactsModel Create(Homegame homegame, IEnumerable<Cashgame> cashgames, Player player);
    }
}