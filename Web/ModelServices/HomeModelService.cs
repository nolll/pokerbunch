using Web.Controllers;
using Web.ModelFactories.HomeModelFactories;
using Web.Models.HomeModels;

namespace Web.ModelServices
{
    public class HomeModelService : IHomeModelService
    {
        private readonly IHomePageModelFactory _homePageModelFactory;

        public HomeModelService(IHomePageModelFactory homePageModelFactory)
        {
            _homePageModelFactory = homePageModelFactory;
        }

        public HomePageModel GetIndexModel()
        {
            return _homePageModelFactory.Create();
        }
    }
}