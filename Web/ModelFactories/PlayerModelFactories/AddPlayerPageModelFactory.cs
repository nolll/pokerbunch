using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class AddPlayerPageModelFactory : IAddPlayerPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public AddPlayerPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public AddPlayerPageModel Create(User user, Homegame homegame, Cashgame runningGame)
        {
            return new AddPlayerPageModel
                {
                    BrowserTitle = "Add Player",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame)
                };
        }

        public AddPlayerPageModel Create(User user, Homegame homegame, Cashgame runningGame, AddPlayerPostModel postModel)
        {
            var model = Create(user, homegame, runningGame);
            model.Name = postModel.Name;
            return model;
        }
    }
}