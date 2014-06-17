using Application.Services;
using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.SharingModelFactories;
using Web.Models.SharingModels;
using Web.Security;

namespace Web.ModelServices
{
    public class SharingModelService : ISharingModelService
    {
        private readonly IAuth _auth;
        private readonly ISharingRepository _sharingRepository;
        private readonly ITwitterRepository _twitterRepository;
        private readonly ISharingIndexPageBuilder _sharingIndexPageBuilder;
        private readonly ISharingTwitterPageBuilder _sharingTwitterPageBuilder;

        public SharingModelService(
            IAuth auth,
            ISharingRepository sharingRepository,
            ITwitterRepository twitterRepository,
            ISharingIndexPageBuilder sharingIndexPageBuilder,
            ISharingTwitterPageBuilder sharingTwitterPageBuilder)
        {
            _auth = auth;
            _sharingRepository = sharingRepository;
            _twitterRepository = twitterRepository;
            _sharingIndexPageBuilder = sharingIndexPageBuilder;
            _sharingTwitterPageBuilder = sharingTwitterPageBuilder;
        }

        public SharingIndexPageModel GetIndexModel()
        {
            var user = _auth.CurrentUser;
            var isSharing = _sharingRepository.IsSharing(user, SocialServiceIdentifier.Twitter);
            return _sharingIndexPageBuilder.Build(isSharing);
        }

        public SharingTwitterPageModel GetTwitterModel()
        {
            var user = _auth.CurrentUser;
            var isSharing = _sharingRepository.IsSharing(user, SocialServiceIdentifier.Twitter);
            var credentials = _twitterRepository.GetCredentials(user);
            return _sharingTwitterPageBuilder.Build(isSharing, credentials);
        }
    }
}