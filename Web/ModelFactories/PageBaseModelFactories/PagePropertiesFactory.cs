using Application.Services;
using Core.Entities;
using Web.ModelFactories.MiscModelFactories;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

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
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var user = _auth.CurrentUser;

            return new PageProperties(
                new UserNavigationModel(user),
                _googleAnalyticsModelFactory.Create(),
                homegameNavModel,
                version,
                BundleConfig.BundleUrl);
        }
    }
}