using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class AddPlayerPageBuilder : IAddPlayerPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IHomegameRepository _homegameRepository;

        public AddPlayerPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameRepository homegameRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameRepository = homegameRepository;
        }

        public AddPlayerPageModel Build(string slug, AddPlayerPostModel postModel = null)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            
            var model = new AddPlayerPageModel
                {
                    BrowserTitle = "Add Player",
                    PageProperties = _pagePropertiesFactory.Create(homegame)
                };
            if (postModel != null)
            {
                model.Name = postModel.Name;
            }
            return model;
        }
    }
}