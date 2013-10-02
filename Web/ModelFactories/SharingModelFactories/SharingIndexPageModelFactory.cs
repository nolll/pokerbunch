using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.SharingModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public class SharingIndexPageModelFactory : ISharingIndexPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public SharingIndexPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public SharingIndexPageModel Create(User user, bool isSharing)
        {
            return new SharingIndexPageModel
                {
                    BrowserTitle = "Sharing",
                    PageProperties = _pagePropertiesFactory.Create(user),
                    IsSharingToTwitter = isSharing,
			        ShareToTwitterSettingsUrl = new TwitterSettingsUrlModel()
                };
        }
    }
}