using Application.Services;
using Application.UseCases.ApplicationContext;
using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomeModels;
using Web.Models.NavigationModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.HomeModelFactories
{
    public class HomePageModelFactory : IHomePageModelFactory
    {
        private readonly IAuth _auth;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IApplicationContextInteractor _applicationContextInteractor;

        public HomePageModelFactory(
            IAuth auth, 
            IHomegameRepository homegameRepository, 
            IPagePropertiesFactory pagePropertiesFactory,
            IApplicationContextInteractor applicationContextInteractor)
        {
            _auth = auth;
            _homegameRepository = homegameRepository;
            _pagePropertiesFactory = pagePropertiesFactory;
            _applicationContextInteractor = applicationContextInteractor;
        }

        public HomePageModel Create()
        {
            var homegame = GetHomegame();
            var applicationContextResult = _applicationContextInteractor.Execute();

            return new HomePageModel
                {
                    BrowserTitle = "Poker Bunch",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
			        IsLoggedIn = _auth.IsAuthenticated,
                    AddHomegameUrl = new AddHomegameUrlModel(),
                    LoginUrl = new LoginUrlModel(),
                    RegisterUrl = new AddUserUrlModel(),
			        AdminNav = new AdminNavigationModel(applicationContextResult)
                };
        }

        private Homegame GetHomegame()
        {
            var games = _homegameRepository.GetByUser(_auth.CurrentUser);
            return games.Count == 1 ? games[0] : null;
        }
    }
}