using App.Services.Interfaces;
using Core.Classes;

namespace App.Services{

	public class InvitationSender : IInvitationSender{
	    private readonly IMessageSender _messageSender;
	    private readonly IInvitationCodeCreator _invitationCodeCreator;
	    private readonly ISettings _settings;
	    private readonly IUrlProvider _urlProvider;

	    public InvitationSender(
            IMessageSender messageSender,
            IInvitationCodeCreator invitationCodeCreator,
			ISettings settings,
            IUrlProvider urlProvider)
	    {
	        _messageSender = messageSender;
	        _invitationCodeCreator = invitationCodeCreator;
	        _settings = settings;
	        _urlProvider = urlProvider;
	    }

	    public void Send(Homegame homegame, Player player, string email){
			var subject = GetSubject(homegame);
			var body = GetBody(homegame, player);
			_messageSender.Send(email, subject, body);
		}

	    private string GetSubject(Homegame homegame){
			return "Invitation to Poker Bunch: " + homegame.DisplayName;
		}

	    private string GetBody(Homegame homegame, Player player){
			var siteUrl = _settings.GetSiteUrl();
	        var joinUrl = _urlProvider.GetJoinHomegameUrl(homegame.Slug);
			var joinUrlStr = siteUrl + joinUrl;
	        var userAddUrl = _urlProvider.GetAddUserUrl();
			var userAddUrlStr = siteUrl + userAddUrl;

			var invitationCode = _invitationCodeCreator.GetCode(player);
			var body = "You have been invited to join the poker game: " + homegame.DisplayName + ".\r\n\r\n" +
				"To accept this invitation, go to " + joinUrlStr +
				" and enter this verification code: " + invitationCode + "\r\n\r\n" +
				"If you don\'t have an account, you can register at " + userAddUrlStr;
			return body;
		}

	}

}