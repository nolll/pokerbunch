using Core.Classes;
using Core.Services;

namespace app\Player{

	public class InvitationSender : IInvitationSender{
	    private readonly MessageSenderFactory _messageSenderFactory;
	    private readonly InvitationCodeCreator _invitationCodeCreator;
	    private readonly Settings _settings;

	    public InvitationSender(
            IMessageSenderFactory messageSenderFactory,
            IInvitationCodeCreator invitationCodeCreator,
			ISettings settings)
	    {
	        _messageSenderFactory = messageSenderFactory;
	        _invitationCodeCreator = invitationCodeCreator;
	        _settings = settings;
	    }

	    public void Send(Homegame homegame, Player player, string email){
			var subject = getSubject(homegame);
			var body = getBody(homegame, player);
			messageSender = _messageSenderFactory.getMessageSender();
			messageSender.send(email, subject, body);
		}

		public string getSubject(Homegame homegame){
			return "Invitation to Poker Bunch: " + homegame.DisplayName;
		}

		public string getBody(Homegame homegame, Player player){
			var siteUrl = _settings.getSiteUrl();
			var joinUrl = new HomegameJoinUrlModel(homegame);
			var joinUrlStr = siteUrl + joinUrl.url;
			var userAddUrl = new UserAddUrlModel();
			var userAddUrlStr = siteUrl + userAddUrl.Url;

			var invitationCode = _invitationCodeCreator.getCode(player);
			var body = "You have been invited to join the poker game: " + homegame.DisplayName + ".\r\n\r\n" +
				"To accept this invitation, go to " + joinUrlStr +
				" and enter this verification code: " + invitationCode + "\r\n\r\n" +
				"If you don\'t have an account, you can register at " + userAddUrlStr;
			return body;
		}

	}

}