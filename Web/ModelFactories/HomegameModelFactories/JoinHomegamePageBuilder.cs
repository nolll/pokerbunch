using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class JoinHomegamePageBuilder : IJoinHomegamePageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IHomegameRepository _homegameRepository;

        public JoinHomegamePageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameRepository homegameRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameRepository = homegameRepository;
        }

        private JoinHomegamePageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);

            return new JoinHomegamePageModel
                {
                    BrowserTitle = "Join Bunch",
                    PageProperties = _pagePropertiesFactory.Create(),
                    Name = homegame.DisplayName
                };
        }

        public JoinHomegamePageModel Build(string slug, JoinHomegamePostModel postModel)
        {
            var model = Build(slug);
            if (postModel != null)
            {
                model.Code = postModel.Code;
            }
            return model;
        }

    }
}