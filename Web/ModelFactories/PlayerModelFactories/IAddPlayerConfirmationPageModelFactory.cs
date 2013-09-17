using Core.Classes;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IAddPlayerConfirmationPageModelFactory
    {
        AddPlayerConfirmationPageModel Create(User user, Homegame homegame, Cashgame runningGame);
    }
}