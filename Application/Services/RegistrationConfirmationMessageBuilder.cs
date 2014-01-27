using App.Services.Interfaces;

namespace App.Services
{
    public class RegistrationConfirmationMessageBuilder : IRegistrationConfirmationMessageBuilder
    {
        private readonly ISettings _settings;
        private readonly IUrlProvider _urlProvider;

        public RegistrationConfirmationMessageBuilder(
            ISettings settings,
            IUrlProvider urlProvider)
        {
            _settings = settings;
            _urlProvider = urlProvider;
        }

        public string GetSubject()
        {
            return "Poker Bunch Registration";
        }

        public string GetBody(string password)
        {
            var siteUrl = _settings.GetSiteUrl();
            var loginUrl = _urlProvider.GetLoginUrl();
            var absoluteLoginUrl = siteUrl + loginUrl;
            return string.Format(BodyFormat, password, absoluteLoginUrl);
        }

        private string BodyFormat
        {
            get
            {
                return
@"Thanks for registering with Poker Bunch.

Here is your password:
{0}

Please sign in here: {1}";
            }
        }
    }
}