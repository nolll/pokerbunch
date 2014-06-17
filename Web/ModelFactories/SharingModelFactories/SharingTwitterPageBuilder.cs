using Application.Urls;
using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.SharingModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public class SharingTwitterPageBuilder : ISharingTwitterPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public SharingTwitterPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public SharingTwitterPageModel Build(bool isSharing, TwitterCredentials credentials)
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

        private Url GetPostUrlModel(bool isSharing)
        {
            if (isSharing)
            {
                return new TwitterStopShareUrl();
            }
            return new TwitterStartShareUrl();
        }
    }
}