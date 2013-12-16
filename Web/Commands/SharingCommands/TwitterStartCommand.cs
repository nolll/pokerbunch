using Core.Classes;
using Core.Repositories;

namespace Web.Commands.SharingCommands
{
    public class TwitterStartCommand : Command
    {
        private readonly ITwitterRepository _twitterRepository;
        private readonly ISharingRepository _sharingRepository;
        private readonly User _user;
        private readonly TwitterCredentials _credentials;

        public TwitterStartCommand(
            ITwitterRepository twitterRepository,
            ISharingRepository sharingRepository,
            User user,
            TwitterCredentials credentials)
        {
            _twitterRepository = twitterRepository;
            _sharingRepository = sharingRepository;
            _user = user;
            _credentials = credentials;
        }

        public override bool Execute()
        {
            _twitterRepository.AddCredentials(_user, _credentials);
            _sharingRepository.AddSharing(_user, SocialServiceIdentifier.Twitter);
            return true;
        }
    }
}