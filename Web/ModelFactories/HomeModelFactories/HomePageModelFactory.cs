using Core.Classes;
using Core.Repositories;
using Core.Services;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomeModels;

namespace Web.ModelFactories.HomeModelFactories
{
    public class HomePageModelFactory : IHomePageModelFactory
    {
        private readonly IUserContext _userContext;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IAdminNavigationModelFactory _adminNavigationFactory;
        private readonly IUrlProvider _urlProvider;

        public HomePageModelFactory(
            IUserContext userContext, 
            IHomegameRepository homegameRepository, 
            ICashgameRepository cashgameRepository,
            IPagePropertiesFactory pagePropertiesFactory,
            IAdminNavigationModelFactory adminNavigationFactory,
            IUrlProvider urlProvider)
        {
            _userContext = userContext;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _pagePropertiesFactory = pagePropertiesFactory;
            _adminNavigationFactory = adminNavigationFactory;
            _urlProvider = urlProvider;
        }

        public HomePageModel Create()
        {
            var homegame = GetHomegame();
            var runningGame = GetRunningGame(homegame);
            var user = _userContext.GetUser();
            return new HomePageModel
                {
                    BrowserTitle = "Poker Bunch",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
			        IsLoggedIn = user != null,
                    AddHomegameUrl = _urlProvider.GetHomegameAddUrl(),
                    LoginUrl = _urlProvider.GetLoginUrl(),
                    RegisterUrl = _urlProvider.GetAddUserUrl(),
			        AdminNav = _adminNavigationFactory.Create(user)
                };
        }

        private Homegame GetHomegame()
        {
            var games = _homegameRepository.GetByUser(_userContext.GetUser());
            return games.Count == 1 ? games[0] : null;
        }

        private Cashgame GetRunningGame(Homegame homegame)
        {
            if (homegame == null)
            {
                return null;
            }
            return _cashgameRepository.GetRunning(homegame);
        }
    }
}