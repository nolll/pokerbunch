using Core.Classes;
using Web.Models.HomegameModels.Details;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IHomegameDetailsPageModelFactory
    {
        HomegameDetailsPageModel Create(User user, Homegame homegame, bool isInManagerMode, Cashgame runningGame = null);
    }
}