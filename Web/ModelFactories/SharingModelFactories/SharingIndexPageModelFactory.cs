using App.Services.Interfaces;
using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.SharingModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public class SharingIndexPageModelFactory : ISharingIndexPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUrlProvider _urlProvider;

        public SharingIndexPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
        }

        public SharingIndexPageModel Create(User user, bool isSharing)
        {
            return new SharingIndexPageModel
                {
                    BrowserTitle = "Sharing",
                    PageProperties = _pagePropertiesFactory.Create(user),
                    IsSharingToTwitter = isSharing,
			        ShareToTwitterSettingsUrl = _urlProvider.GetTwitterSettingsUrl()
                };
        }
    }
}