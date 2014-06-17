using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class AddPlayerConfirmationPageBuilder : IAddPlayerConfirmationPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public AddPlayerConfirmationPageBuilder(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public AddPlayerConfirmationPageModel Build(Homegame homegame)
        {
            return new AddPlayerConfirmationPageModel
                {
                    BrowserTitle = "Player Added",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    HomegameName = homegame.DisplayName
                };
        }
    }
}