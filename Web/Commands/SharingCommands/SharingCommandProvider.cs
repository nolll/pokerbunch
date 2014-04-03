using Application.Services;
using Core.Repositories;
using Web.Security;

namespace Web.Commands.SharingCommands
{
    public class SharingCommandProvider : ISharingCommandProvider
    {
        private readonly IAuth _auth;
        private readonly ISharingRepository _sharingRepository;
        private readonly ITwitterRepository _twitterRepository;
        private readonly ITwitterIntegration _twitterIntegration;

        public SharingCommandProvider(
            IAuth auth,
            ISharingRepository sharingRepository,
            ITwitterRepository twitterRepository,
            ITwitterIntegration twitterIntegration)
        {
            _auth = auth;
            _sharingRepository = sharingRepository;
            _twitterRepository = twitterRepository;
            _twitterIntegration = twitterIntegration;
        }

        public Command GetTwitterStopCommand()
        {
            var user = _auth.GetUser();
            return new TwitterStopCommand(_sharingRepository, user);
        }

        public Command GetStartCommand(string token, string verifier)
        {
            var user = _auth.GetUser();
            var twitterCredentials = _twitterIntegration.GetCredentials(token, verifier);

            return new TwitterStartCommand(_twitterRepository, _sharingRepository, user, twitterCredentials);
        }
    }
}