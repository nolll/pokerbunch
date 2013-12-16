using Core.Classes;
using Core.Repositories;

namespace Web.Commands.SharingCommands
{
    public class TwitterStopCommand : Command
    {
        private readonly ISharingRepository _sharingRepository;
        private readonly User _user;

        public TwitterStopCommand(ISharingRepository sharingRepository, User user)
        {
            _sharingRepository = sharingRepository;
            _user = user;
        }

        public override bool Execute()
        {
            _sharingRepository.RemoveSharing(_user, SocialServiceIdentifier.Twitter);
            return true;
        }
    }
}