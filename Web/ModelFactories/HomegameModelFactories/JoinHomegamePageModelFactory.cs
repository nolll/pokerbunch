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

        private JoinHomegamePageModel Create(Homegame homegame)
        {
            return new JoinHomegamePageModel
                {
                    BrowserTitle = "Join Bunch",
                    PageProperties = _pagePropertiesFactory.Create(),
                    Name = homegame.DisplayName
                };
        }

        public JoinHomegamePageModel Create(Homegame homegame, JoinHomegamePostModel postModel)
        {
            var model = Create(homegame);
            if (postModel != null)
            {
                model.Code = postModel.Code;
            }
            return model;
        }

    }
}