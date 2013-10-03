using Core.Classes;
using Web.ModelFactories.MiscModelFactories;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.PageBaseModelFactories
{
    public class PagePropertiesFactory : IPagePropertiesFactory
    {
        private readonly IGoogleAnalyticsModelFactory _googleAnalyticsModelFactory;

        public PagePropertiesFactory(IGoogleAnalyticsModelFactory googleAnalyticsModelFactory)
        {
            _googleAnalyticsModelFactory = googleAnalyticsModelFactory;
        }

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
			        GoogleAnalyticsModel = _googleAnalyticsModelFactory.Create(),
                    HomegameNavModel = homegame != null ? new HomegameNavigationModel(homegame, runningGame) : null
                };
        }
    }
}