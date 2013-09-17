using Core.Classes;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IAddPlayerPageModelFactory
    {
        AddPlayerPageModel Create(User user, Homegame homegame, Cashgame runningGame);
        AddPlayerPageModel Create(User user, Homegame homegame, Cashgame runningGame, AddPlayerPostModel postModel);
    }
}