using Application.Services.Interfaces;
using Core.Classes;

namespace Application.Services
{
    public class InvitationMessageBuilder : IInvitationMessageBuilder
    {
        private readonly ISettings _settings;
        private readonly IUrlProvider _urlProvider;
        private readonly IInvitationCodeCreator _invitationCodeCreator;

        public InvitationMessageBuilder(
            ISettings settings,
            IUrlProvider urlProvider,
            IInvitationCodeCreator invitationCodeCreator)
        {
            _settings = settings;
            _urlProvider = urlProvider;
            _invitationCodeCreator = invitationCodeCreator;
        }

        public string GetSubject(Homegame homegame)
        {
            return string.Format("Invitation to Poker Bunch: {0}", homegame.DisplayName);
        }

        public string GetBody(Homegame homegame, Player player)
        {
            var siteUrl = _settings.GetSiteUrl();
            var joinUrl = _urlProvider.GetJoinHomegameUrl(homegame.Slug);
            var fullJoinUrl = siteUrl + joinUrl;
            var userAddUrl = _urlProvider.GetAddUserUrl();
            var fullAddUrl = siteUrl + userAddUrl;
            var invitationCode = _invitationCodeCreator.GetCode(player);
            return string.Format(BodyFormat, homegame.DisplayName, fullJoinUrl, invitationCode, fullAddUrl);
        }

        private const string BodyFormat =
@"You have been invited to join the poker game: {0}.

To accept this invitation, go to {1} and enter this verification code: {2}

If you don't have an account, you can register at {3}";
    }
}