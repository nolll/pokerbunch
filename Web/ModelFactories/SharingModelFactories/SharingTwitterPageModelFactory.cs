using Core.Classes;
using Core.Services;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.SharingModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public class SharingTwitterPageModelFactory : ISharingTwitterPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUrlProvider _urlProvider;

        public SharingTwitterPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
        }

        public SharingTwitterPageModel Create(User user, bool isSharing, TwitterCredentials credentials)
        {
            var twitterName = isSharing && credentials != null ? credentials.TwitterName : null;

            return new SharingTwitterPageModel
                {
                    BrowserTitle = "Twitter Sharing",
                    PageProperties = _pagePropertiesFactory.Create(user),
                    IsSharing = isSharing,
                    TwitterName = twitterName,
                    PostUrl = GetPostUrlModel(isSharing)
                };
        }

        private string GetPostUrlModel(bool isSharing)
        {
            if (isSharing)
            {
                return _urlProvider.GetTwitterStopShareUrl();
            }
            return _urlProvider.GetTwitterStartShareUrl();
        }
    }
}