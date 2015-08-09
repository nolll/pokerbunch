using Core.Entities;
using Core.Urls;

namespace Core.Services
{
    public class InvitationMessage : IMessage
    {
        private readonly Bunch _bunch;
        private readonly Player _player;
        private readonly string _registerUrl;

        public InvitationMessage(Bunch bunch, Player player, string registerUrl)
        {
            _bunch = bunch;
            _player = player;
            _registerUrl = registerUrl;
        }

        public string Subject
        {
            get { return string.Format("Invitation to Poker Bunch: {0}", _bunch.DisplayName); }
        }

        public string Body
        {
            get
            {
                var joinUrl = new JoinBunchUrl(_bunch.Slug).Absolute;
                var invitationCode = InvitationCodeCreator.GetCode(_player);
                return string.Format(BodyFormat, _bunch.DisplayName, joinUrl, invitationCode, _registerUrl);
            }
        }

        private const string BodyFormat =
@"You have been invited to join the poker game: {0}.

To accept this invitation, go to {1} and enter this verification code: {2}

If you don't have an account, you can register at {3}";
    }
}