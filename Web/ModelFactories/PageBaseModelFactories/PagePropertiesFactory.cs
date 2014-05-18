using Application.UseCases.CashgameContext;
using Core.Entities;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.NavigationModelFactories;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;
using Web.Security;

namespace Web.ModelFactories.PageBaseModelFactories
{
    public class PagePropertiesFactory : IPagePropertiesFactory
    {
        private readonly IGoogleAnalyticsModelFactory _googleAnalyticsModelFactory;
        private readonly IHomegameNavigationModelFactory _homegameNavigationModelFactory;
        private readonly IUserNavigationModelFactory _userNavigationModelFactory;
        private readonly IAuth _auth;

        public PagePropertiesFactory(
            IGoogleAnalyticsModelFactory googleAnalyticsModelFactory,
            IHomegameNavigationModelFactory homegameNavigationModelFactory,
            IUserNavigationModelFactory userNavigationModelFactory,
            IAuth auth)
        {
            _googleAnalyticsModelFactory = googleAnalyticsModelFactory;
            _homegameNavigationModelFactory = homegameNavigationModelFactory;
            _userNavigationModelFactory = userNavigationModelFactory;
            _auth = auth;
        }

        public PageProperties Create(Homegame homegame)
        {
            var homegameNavModel = homegame != null ? _homegameNavigationModelFactory.Create(homegame) : null;
            return Create(homegameNavModel);
        }

        public PageProperties Create(BunchContextResult bunchContextResult)
        {
            var homegameNavModel = bunchContextResult != null ? _homegameNavigationModelFactory.Create(bunchContextResult) : null;
            return Create(homegameNavModel);
        }

        private PageProperties Create(HomegameNavigationModel homegameNavigationModel)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var user = _auth.CurrentUser;

            return new PageProperties
            {
                UserNavModel = _userNavigationModelFactory.Create(user),
                GoogleAnalyticsModel = _googleAnalyticsModelFactory.Create(),
                HomegameNavModel = homegameNavigationModel,
                Version = version,
                CssUrl = BundleConfig.BundleUrl
            };
        }
    }
}