using Core.Classes;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.PageBaseModelFactories
{
    public class PagePropertiesFactory : IPagePropertiesFactory
    {
        public PageProperties Create()
        {
            return new PageProperties(null);
        }

        public PageProperties Create(User user)
        {
            return new PageProperties(user);
        }

        public PageProperties Create(User user, Homegame homegame, Cashgame runningGame = null)
        {
            return new PageProperties(user, homegame, runningGame);
        }
    }
}