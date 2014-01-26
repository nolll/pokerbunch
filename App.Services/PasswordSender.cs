using App.Services.Interfaces;
using Core.Classes;

namespace App.Services{

	public class PasswordSender : IPasswordSender
    {
	    private readonly IMessageSender _messageSender;
	    private readonly ISettings _settings;
	    private readonly IUrlProvider _urlProvider;

	    public PasswordSender(
            IMessageSender messageSender,
            ISettings settings,
            IUrlProvider urlProvider)
	    {
	        _messageSender = messageSender;
	        _settings = settings;
	        _urlProvider = urlProvider;
	    }

	    public void Send(User user, string password){
			const string subject = "Poker Bunch password recovery";
			var body = getBody(password);
			_messageSender.Send(user.Email, subject, body);
		}

	    private string getBody(string password){
			var siteUrl = _settings.GetSiteUrl();
			var loginUrl = _urlProvider.GetLoginUrl();
			var loginUrlStr = siteUrl + loginUrl;
			var body = "Here is your new password for Poker Bunch:\r\n" +
				password + "\r\n\r\n" +
				"Please sign in here: " + loginUrlStr;

			return body;
		}

	}

}