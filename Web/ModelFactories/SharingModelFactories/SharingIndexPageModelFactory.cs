using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.SharingModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public class SharingIndexPageModelFactory : ISharingIndexPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public SharingIndexPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
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