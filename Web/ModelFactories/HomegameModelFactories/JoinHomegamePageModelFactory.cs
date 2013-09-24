using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class JoinHomegamePageModelFactory : IJoinHomegamePageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public JoinHomegamePageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public JoinHomegamePageModel Create(User user)
        {
            return new JoinHomegamePageModel
                {
                    BrowserTitle = "Join Homegame",
                    PageProperties = _pagePropertiesFactory.Create(user)
                };
        }

        public JoinHomegamePageModel Create(User user, JoinHomegamePostModel postModel)
        {
            var model = Create(user);
            model.Code = postModel.Code;
            return model;
        }

    }
}