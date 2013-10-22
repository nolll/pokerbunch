using Core.Classes;
using Web.Models.PlayerModels.Listing;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IPlayerItemModelFactory
    {
        PlayerItemModel Create(Homegame homegame, Player player);
    }
}