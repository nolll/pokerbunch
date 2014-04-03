using Core.Classes;
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
        private readonly ISharingIndexPageModelFactory _sharingIndexPageModelFactory;
        private readonly ISharingTwitterPageModelFactory _sharingTwitterPageModelFactory;

        public SharingModelService(
            IAuth auth,
            ISharingRepository sharingRepository,
            ITwitterRepository twitterRepository,
            ISharingIndexPageModelFactory sharingIndexPageModelFactory,
            ISharingTwitterPageModelFactory sharingTwitterPageModelFactory)
        {
            _auth = auth;
            _sharingRepository = sharingRepository;
            _twitterRepository = twitterRepository;
            _sharingIndexPageModelFactory = sharingIndexPageModelFactory;
            _sharingTwitterPageModelFactory = sharingTwitterPageModelFactory;
        }

        public SharingIndexPageModel GetIndexModel()
        {
            var user = _auth.GetUser();
            var isSharing = _sharingRepository.IsSharing(user, SocialServiceIdentifier.Twitter);
            return _sharingIndexPageModelFactory.Create(isSharing);
        }

        public SharingTwitterPageModel GetTwitterModel()
        {
            var user = _auth.GetUser();
            var isSharing = _sharingRepository.IsSharing(user, SocialServiceIdentifier.Twitter);
            var credentials = _twitterRepository.GetCredentials(user);
            return _sharingTwitterPageModelFactory.Create(isSharing, credentials);
        }
    }
}