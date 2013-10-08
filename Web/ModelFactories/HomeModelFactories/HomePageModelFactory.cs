using Core.Classes;
using Core.Repositories;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomeModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.HomeModelFactories
{
    public class HomePageModelFactory : IHomePageModelFactory
    {
        private readonly IUserContext _userContext;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IAdminNavigationModelFactory _adminNavigationFactory;

        public HomePageModelFactory(
            IUserContext userContext, 
            IHomegameRepository homegameRepository, 
            ICashgameRepository cashgameRepository,
            IPagePropertiesFactory pagePropertiesFactory,
            IAdminNavigationModelFactory adminNavigationFactory)
        {
            _userContext = userContext;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _pagePropertiesFactory = pagePropertiesFactory;
            _adminNavigationFactory = adminNavigationFactory;
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
                    AddHomegameUrl = new HomegameAddUrlModel(),
                    LoginUrl = new AuthLoginUrlModel(),
                    RegisterUrl = new UserAddUrlModel(),
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