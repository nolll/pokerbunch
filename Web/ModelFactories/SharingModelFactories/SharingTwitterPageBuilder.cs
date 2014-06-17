using Application.Services;
using Application.Urls;
using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.SharingModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public class SharingTwitterPageBuilder : ISharingTwitterPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IAuth _auth;
        private readonly ISharingRepository _sharingRepository;
        private readonly ITwitterRepository _twitterRepository;

        public SharingTwitterPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IAuth auth,
            ISharingRepository sharingRepository,
            ITwitterRepository twitterRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _auth = auth;
            _sharingRepository = sharingRepository;
            _twitterRepository = twitterRepository;
        }

        public SharingTwitterPageModel Build()
        {
            var user = _auth.CurrentUser;
            var isSharing = _sharingRepository.IsSharing(user, SocialServiceIdentifier.Twitter);
            var credentials = _twitterRepository.GetCredentials(user);
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