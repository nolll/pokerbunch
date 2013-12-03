using Core.Classes;
using Web.Models.PlayerModels.List;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IPlayerItemModelFactory
    {
        PlayerItemModel Create(Homegame homegame, Player player);
    }
}