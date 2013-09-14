using Core.Classes;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.PageBaseModelFactories
{
    public interface IPagePropertiesFactory
    {
        PageProperties Create();
        PageProperties Create(User user);
        PageProperties Create(User user, Homegame homegame, Cashgame runningGame = null);
    }
}