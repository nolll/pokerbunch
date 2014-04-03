﻿using Core.Classes;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.NavigationModelFactories;
using Web.Models.PageBaseModels;
using Web.Security;

namespace Web.ModelFactories.PageBaseModelFactories
{
    public class PagePropertiesFactory : IPagePropertiesFactory
    {
        private readonly IGoogleAnalyticsModelFactory _googleAnalyticsModelFactory;
        private readonly IHomegameNavigationModelFactory _homegameNavigationModelFactory;
        private readonly IUserNavigationModelFactory _userNavigationModelFactory;
        private readonly IAuthentication _authentication;

        public PagePropertiesFactory(
            IGoogleAnalyticsModelFactory googleAnalyticsModelFactory,
            IHomegameNavigationModelFactory homegameNavigationModelFactory,
            IUserNavigationModelFactory userNavigationModelFactory,
            IAuthentication authentication)
        {
            _googleAnalyticsModelFactory = googleAnalyticsModelFactory;
            _homegameNavigationModelFactory = homegameNavigationModelFactory;
            _userNavigationModelFactory = userNavigationModelFactory;
            _authentication = authentication;
        }

        public PageProperties Create()
        {
            return Create(null);
        }

        public PageProperties Create(Homegame homegame)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var user = _authentication.GetUser();

            return new PageProperties
                {
                    UserNavModel = _userNavigationModelFactory.Create(user),
			        GoogleAnalyticsModel = _googleAnalyticsModelFactory.Create(),
                    HomegameNavModel = homegame != null ? _homegameNavigationModelFactory.Create(homegame) : null,
                    Version = version,
                    CssUrl = BundleConfig.BundleUrl
                    //CssUrl = string.Format("/-/css/{0}", version.Replace(".", "-"))
                };
        }
    }
}