using Core.Classes;
using Infrastructure.Config;
using Infrastructure.Services;
using Web.Models.UrlModels;
using Web.Services.Interfaces;

namespace Web.Services{

	public class InvitationSender : IInvitationSender{
	    private readonly IMessageSender _messageSender;
	    private readonly IInvitationCodeCreator _invitationCodeCreator;
	    private readonly ISettings _settings;

	    public InvitationSender(
            IMessageSender messageSender,
            IInvitationCodeCreator invitationCodeCreator,
			ISettings settings)
	    {
	        _messageSender = messageSender;
	        _invitationCodeCreator = invitationCodeCreator;
	        _settings = settings;
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
			var joinUrl = new HomegameJoinUrlModel(homegame);
			var joinUrlStr = siteUrl + joinUrl.Url;
			var userAddUrl = new UserAddUrlModel();
			var userAddUrlStr = siteUrl + userAddUrl.Url;

			var invitationCode = _invitationCodeCreator.GetCode(player);
			var body = "You have been invited to join the poker game: " + homegame.DisplayName + ".\r\n\r\n" +
				"To accept this invitation, go to " + joinUrlStr +
				" and enter this verification code: " + invitationCode + "\r\n\r\n" +
				"If you don\'t have an account, you can register at " + userAddUrlStr;
			return body;
		}

	}

}