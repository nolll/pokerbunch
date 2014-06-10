using Application.Services;
using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.SharingModels;
using Web.Models.UrlModels;
using Web.Services;

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

        public SharingTwitterPageModel Create(bool isSharing, TwitterCredentials credentials)
        {
            var twitterName = isSharing && credentials != null ? credentials.TwitterName : null;

            return new SharingTwitterPageModel
                {
                    BrowserTitle = "Twitter Sharing",
                    PageProperties = _pagePropertiesFactory.Create(),
                    IsSharing = isSharing,
                    TwitterName = twitterName,
                    PostUrl = GetPostUrlModel(isSharing)
                };
        }

        private UrlModel GetPostUrlModel(bool isSharing)
        {
            if (isSharing)
            {
                return new TwitterStopShareUrlModel();
            }
            return new TwitterStartShareUrlModel();
        }
    }
}