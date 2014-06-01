using Application.UseCases.CashgameContext;
using Core.Entities;
using Web.ModelFactories.MiscModelFactories;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;
using Web.Security;

namespace Web.ModelFactories.PageBaseModelFactories
{
    public class PagePropertiesFactory : IPagePropertiesFactory
    {
        private readonly IGoogleAnalyticsModelFactory _googleAnalyticsModelFactory;
        private readonly IAuth _auth;

        public PagePropertiesFactory(
            IGoogleAnalyticsModelFactory googleAnalyticsModelFactory,
            IAuth auth)
        {
            _googleAnalyticsModelFactory = googleAnalyticsModelFactory;
            _auth = auth;
        }

        public PageProperties Create(Homegame homegame)
        {
            var homegameNavModel = homegame != null ? new HomegameNavigationModel(homegame) : null;
            return Create(homegameNavModel);
        }

        public PageProperties Create(BunchContextResult bunchContextResult)
        {
            var homegameNavModel = bunchContextResult != null ? new HomegameNavigationModel(bunchContextResult) : null;
            return Create(homegameNavModel);
        }

        private PageProperties Create(HomegameNavigationModel homegameNavigationModel)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var user = _auth.CurrentUser;

            return new PageProperties
            {
                UserNavModel = new UserNavigationModel(user),
                GoogleAnalyticsModel = _googleAnalyticsModelFactory.Create(),
                HomegameNavModel = homegameNavigationModel,
                Version = version,
                CssUrl = BundleConfig.BundleUrl
            };
        }
    }
}