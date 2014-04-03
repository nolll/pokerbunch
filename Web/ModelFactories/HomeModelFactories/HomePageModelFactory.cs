using Application.Services;
using Core.Classes;
using Core.Repositories;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomeModels;
using Web.Security;

namespace Web.ModelFactories.HomeModelFactories
{
    public class HomePageModelFactory : IHomePageModelFactory
    {
        private readonly IAuthentication _authentication;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IAdminNavigationModelFactory _adminNavigationFactory;
        private readonly IUrlProvider _urlProvider;

        public HomePageModelFactory(
            IAuthentication authentication, 
            IHomegameRepository homegameRepository, 
            IPagePropertiesFactory pagePropertiesFactory,
            IAdminNavigationModelFactory adminNavigationFactory,
            IUrlProvider urlProvider)
        {
            _authentication = authentication;
            _homegameRepository = homegameRepository;
            _pagePropertiesFactory = pagePropertiesFactory;
            _adminNavigationFactory = adminNavigationFactory;
            _urlProvider = urlProvider;
        }

        public HomePageModel Create()
        {
            var homegame = GetHomegame();
            var user = _authentication.GetUser(); // todo: use isAdmin and isAuthenticated instead
            return new HomePageModel
                {
                    BrowserTitle = "Poker Bunch",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
			        IsLoggedIn = user != null,
                    AddHomegameUrl = _urlProvider.GetHomegameAddUrl(),
                    LoginUrl = _urlProvider.GetLoginUrl(),
                    RegisterUrl = _urlProvider.GetAddUserUrl(),
			        AdminNav = _adminNavigationFactory.Create(user)
                };
        }

        private Homegame GetHomegame()
        {
            var games = _homegameRepository.GetByUser(_authentication.GetUser());
            return games.Count == 1 ? games[0] : null;
        }
    }
}