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

        public AddPlayerPageModel Create(User user, Homegame homegame, Cashgame runningGame, AddPlayerPostModel postModel = null)
        {
            var model = new AddPlayerPageModel
                {
                    BrowserTitle = "Add Player",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame)
                };
            if (postModel != null)
            {
                model.Name = postModel.Name;
            }
            return model;
        }
    }
}