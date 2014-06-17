using Application.Urls;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.SharingModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public class SharingIndexPageBuilder : ISharingIndexPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public SharingIndexPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public SharingIndexPageModel Build(bool isSharing)
        {
            return new SharingIndexPageModel
                {
                    BrowserTitle = "Sharing",
                    PageProperties = _pagePropertiesFactory.Create(),
                    IsSharingToTwitter = isSharing,
			        ShareToTwitterSettingsUrl = new TwitterSettingsUrl()
                };
        }
    }
}