using Core.Entities;
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

        public AddPlayerPageModel Create(Homegame homegame, AddPlayerPostModel postModel = null)
        {
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