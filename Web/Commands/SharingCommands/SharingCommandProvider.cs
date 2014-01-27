using Application.Services.Interfaces;
using Core.Repositories;

namespace Web.Commands.SharingCommands
{
    public class SharingCommandProvider : ISharingCommandProvider
    {
        private readonly IAuthentication _authentication;
        private readonly ISharingRepository _sharingRepository;
        private readonly ITwitterRepository _twitterRepository;
        private readonly ITwitterIntegration _twitterIntegration;

        public SharingCommandProvider(
            IAuthentication authentication,
            ISharingRepository sharingRepository,
            ITwitterRepository twitterRepository,
            ITwitterIntegration twitterIntegration)
        {
            _authentication = authentication;
            _sharingRepository = sharingRepository;
            _twitterRepository = twitterRepository;
            _twitterIntegration = twitterIntegration;
        }

        public Command GetTwitterStopCommand()
        {
            var user = _authentication.GetUser();
            return new TwitterStopCommand(_sharingRepository, user);
        }

        public Command GetStartCommand(string token, string verifier)
        {
            var user = _authentication.GetUser();
            var twitterCredentials = _twitterIntegration.GetCredentials(token, verifier);

            return new TwitterStartCommand(_twitterRepository, _sharingRepository, user, twitterCredentials);
        }
    }
}