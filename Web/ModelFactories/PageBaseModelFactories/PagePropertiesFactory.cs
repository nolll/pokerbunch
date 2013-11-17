using Core.Classes;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.NavigationModelFactories;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.PageBaseModelFactories
{
    public class PagePropertiesFactory : IPagePropertiesFactory
    {
        private readonly IGoogleAnalyticsModelFactory _googleAnalyticsModelFactory;
        private readonly IHomegameNavigationModelFactory _homegameNavigationModelFactory;
        private readonly IUserNavigationModelFactory _userNavigationModelFactory;

        public PagePropertiesFactory(
            IGoogleAnalyticsModelFactory googleAnalyticsModelFactory,
            IHomegameNavigationModelFactory homegameNavigationModelFactory,
            IUserNavigationModelFactory userNavigationModelFactory)
        {
            _googleAnalyticsModelFactory = googleAnalyticsModelFactory;
            _homegameNavigationModelFactory = homegameNavigationModelFactory;
            _userNavigationModelFactory = userNavigationModelFactory;
        }

        public PageProperties Create()
        {
            return Create(null);
        }

        public PageProperties Create(User user)
        {
            return Create(user, null);
        }

        public PageProperties Create(User user, Homegame homegame)
        {
            return new PageProperties
                {
                    UserNavModel = _userNavigationModelFactory.Create(user),
			        GoogleAnalyticsModel = _googleAnalyticsModelFactory.Create(),
                    HomegameNavModel = homegame != null ? _homegameNavigationModelFactory.Create(homegame) : null
                };
        }
    }
}