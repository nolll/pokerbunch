using Core.Classes;

namespace Core.Services{

	public class RegistrationConfirmationSender : IRegistrationConfirmationSender
    {
	    private readonly IMessageSender _messageSender;
	    private readonly ISettings _settings;
	    private readonly IUrlProvider _urlProvider;

	    public RegistrationConfirmationSender(
            IMessageSender messageSender,
            ISettings settings,
            IUrlProvider urlProvider)
	    {
	        _messageSender = messageSender;
	        _settings = settings;
	        _urlProvider = urlProvider;
	    }

		public void Send(User user, string password){
			var subject = GetSubject();
			var body = GetBody(password);
			_messageSender.Send(user.Email, subject, body);
		}

	    private string GetSubject(){
			return "Poker Bunch Registration";
		}

	    private string GetBody(string password){
			var siteUrl = _settings.GetSiteUrl();
			var loginUrl = _urlProvider.GetAuthLoginUrl();
			var loginUrlStr = siteUrl + loginUrl;
			var body = "Thanks for registering with Poker Bunch.\r\n\r\n" +
				"Here is your password:\r\n" +
				password + "\r\n\r\n" +
				"Please sign in here: " + loginUrlStr;

			return body;
		}

	}

}