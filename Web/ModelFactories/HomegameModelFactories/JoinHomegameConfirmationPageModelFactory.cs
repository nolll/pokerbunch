using Application.Urls;
using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Join;
using Web.Models.UrlModels;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class JoinHomegameConfirmationPageModelFactory : IJoinHomegameConfirmationPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public JoinHomegameConfirmationPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public JoinHomegameConfirmationPageModel Create(Homegame homegame)
        {
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