using Core.Classes;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.NavigationModelFactories;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.PageBaseModelFactories
{
    public class PagePropertiesFactory : IPagePropertiesFactory
    {
        private readonly IGoogleAnalyticsModelFactory _googleAnalyticsModelFactory;
        private readonly IHomegameNavigationModelFactory _homegameNavigationModelFactory;

        public PagePropertiesFactory(
            IGoogleAnalyticsModelFactory googleAnalyticsModelFactory,
            IHomegameNavigationModelFactory homegameNavigationModelFactory)
        {
            _googleAnalyticsModelFactory = googleAnalyticsModelFactory;
            _homegameNavigationModelFactory = homegameNavigationModelFactory;
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
                    HomegameNavModel = homegame != null ? _homegameNavigationModelFactory.Create(homegame, runningGame) : null
                };
        }
    }
}