using Core.Classes;
using Web.Models.MiscModels;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.PageBaseModelFactories
{
    public class PagePropertiesFactory : IPagePropertiesFactory
    {
        public PageProperties Create()
        {
            return Create(null);
        }

        public PageProperties Create(User user)
        {
            return Create(user, null);
        }

        public PageProperties Create(User user, Homegame homegame, Cashgame runningGame = null)
        {
            return new PageProperties
                {
                    UserNavModel = new UserNavigationModel(user),
			        GoogleAnalyticsModel = new GoogleAnalyticsModel(),
                    HomegameNavModel = homegame != null ? new HomegameNavigationModel(homegame, runningGame) : null
                };
        }
    }
}