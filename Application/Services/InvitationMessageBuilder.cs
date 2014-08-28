using Application.Urls;
using Core.Entities;

namespace Application.Services
{
    public static class InvitationMessageBuilder
    {
        public static string GetSubject(Bunch bunch)
        {
            return string.Format("Invitation to Poker Bunch: {0}", bunch.DisplayName);
        }

        public static string GetBody(Bunch bunch, Player player)
        {
            var joinUrl = new JoinBunchUrl(bunch.Slug).Absolute;
            var addUserUrl = new AddUserUrl().Absolute;
            var invitationCode = InvitationCodeCreator.GetCode(player);
            return string.Format(BodyFormat, bunch.DisplayName, joinUrl, invitationCode, addUserUrl);
        }

        private const string BodyFormat =
@"You have been invited to join the poker game: {0}.

To accept this invitation, go to {1} and enter this verification code: {2}

If you don't have an account, you can register at {3}";
    }
}