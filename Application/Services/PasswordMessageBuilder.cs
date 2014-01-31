namespace Application.Services
{
    public class PasswordMessageBuilder : IPasswordMessageBuilder
    {
        private readonly ISettings _settings;
        private readonly IUrlProvider _urlProvider;

        public PasswordMessageBuilder(
            ISettings settings,
            IUrlProvider urlProvider)
        {
            _settings = settings;
            _urlProvider = urlProvider;
        }

        public string GetSubject()
        {
            return "Poker Bunch password recovery";
        }

        public string GetBody(string password)
        {
            var siteUrl = _settings.GetSiteUrl();
            var loginUrl = _urlProvider.GetLoginUrl();
            var loginUrlStr = siteUrl + loginUrl;
            return string.Format(BodyFormat, password, loginUrlStr);
        }

        private const string BodyFormat =
@"Here is your new password for Poker Bunch:
{0}

Please sign in here: {1}";

    }
}