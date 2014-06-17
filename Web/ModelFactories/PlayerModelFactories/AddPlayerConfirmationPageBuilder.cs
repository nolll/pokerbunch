using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class AddPlayerConfirmationPageBuilder : IAddPlayerConfirmationPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IHomegameRepository _homegameRepository;

        public AddPlayerConfirmationPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameRepository homegameRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameRepository = homegameRepository;
        }

        public AddPlayerConfirmationPageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            
            return new AddPlayerConfirmationPageModel
                {
                    BrowserTitle = "Player Added",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    HomegameName = homegame.DisplayName
                };
        }
    }
}