using Core.Classes;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class AddPlayerConfirmationPageModelFactory : IAddPlayerConfirmationPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public AddPlayerConfirmationPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public AddPlayerConfirmationPageModel Create(User user, Homegame homegame, Cashgame runningGame)
        {
            return new AddPlayerConfirmationPageModel
                {
                    BrowserTitle = "Player Added",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
                    HomegameName = homegame.DisplayName
                };
        }
    }
}