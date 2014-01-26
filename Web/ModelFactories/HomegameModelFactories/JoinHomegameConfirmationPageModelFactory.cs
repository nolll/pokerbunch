using App.Services.Interfaces;
using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class JoinHomegameConfirmationPageModelFactory : IJoinHomegameConfirmationPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUrlProvider _urlProvider;

        public JoinHomegameConfirmationPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
        }

        public JoinHomegameConfirmationPageModel Create(User user, Homegame homegame)
        {
            return new JoinHomegameConfirmationPageModel
                {
                    BrowserTitle = "Welcome",
                    PageProperties = _pagePropertiesFactory.Create(user),
                    BunchUrl = _urlProvider.GetHomegameDetailsUrl(homegame.Slug),
                    BunchName = homegame.DisplayName
                };
        }
    }
}