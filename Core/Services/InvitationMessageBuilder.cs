using Core.Entities;

namespace Core.Services
{
    public class InvitationMessage : IMessage
    {
        private readonly string _bunchName;
        private readonly Player _player;
        private readonly string _registerUrl;
        private readonly string _joinUrl;

        public InvitationMessage(string bunchName, Player player, string registerUrl, string joinUrl)
        {
            _bunchName = bunchName;
            _player = player;
            _registerUrl = registerUrl;
            _joinUrl = joinUrl;
        }

        public string Subject
        {
            get { return string.Format("Invitation to Poker Bunch: {0}", _bunchName); }
        }

        public string Body
        {
            get
            {
                var invitationCode = InvitationCodeCreator.GetCode(_player);
                return string.Format(BodyFormat, _bunchName, _joinUrl, invitationCode, _registerUrl);
            }
        }

        private const string BodyFormat =
@"You have been invited to join the poker game: {0}.

To accept this invitation, go to {1} and enter this verification code: {2}

If you don't have an account, you can register at {3}";
    }
}