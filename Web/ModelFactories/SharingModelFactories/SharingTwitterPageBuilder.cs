using Application.Services;
using Application.Urls;
using Application.UseCases.AppContext;
using Core.Entities;
using Core.Repositories;
using Web.Models.PageBaseModels;
using Web.Models.SharingModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public class SharingTwitterPageBuilder : ISharingTwitterPageBuilder
    {
        private readonly IAuth _auth;
        private readonly ISharingRepository _sharingRepository;
        private readonly ITwitterRepository _twitterRepository;
        private readonly IAppContextInteractor _contextInteractor;

        public SharingTwitterPageBuilder(
            IAuth auth,
            ISharingRepository sharingRepository,
            ITwitterRepository twitterRepository,
            IAppContextInteractor contextInteractor)
        {
            _auth = auth;
            _sharingRepository = sharingRepository;
            _twitterRepository = twitterRepository;
            _contextInteractor = contextInteractor;
        }

        public SharingTwitterPageModel Build()
        {
            var user = _auth.CurrentUser;
            var isSharing = _sharingRepository.IsSharing(user, SocialServiceIdentifier.Twitter);
            var credentials = _twitterRepository.GetCredentials(user);
            var twitterName = isSharing && credentials != null ? credentials.TwitterName : null;

            var contextResult = _contextInteractor.Execute();

            return new SharingTwitterPageModel(contextResult)
                {
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