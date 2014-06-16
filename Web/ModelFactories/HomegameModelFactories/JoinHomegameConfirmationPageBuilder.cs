using Application.Urls;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class JoinHomegameConfirmationPageBuilder : IJoinHomegameConfirmationPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IHomegameRepository _homegameRepository;

        public JoinHomegameConfirmationPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameRepository homegameRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameRepository = homegameRepository;
        }

        public JoinHomegameConfirmationPageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);

            return new JoinHomegameConfirmationPageModel
                {
                    BrowserTitle = "Welcome",
                    PageProperties = _pagePropertiesFactory.Create(),
                    BunchUrl = new HomegameDetailsUrl(homegame.Slug),
                    BunchName = homegame.DisplayName
                };
        }
    }
}