using Application.Services;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.SharingModels;
using Web.Services;

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

        public SharingIndexPageModel Create(bool isSharing)
        {
            return new SharingIndexPageModel
                {
                    BrowserTitle = "Sharing",
                    PageProperties = _pagePropertiesFactory.Create(),
                    IsSharingToTwitter = isSharing,
			        ShareToTwitterSettingsUrl = new TwitterSettingsUrlModel()
                };
        }
    }
}