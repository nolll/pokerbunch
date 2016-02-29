namespace Core.Services
{
    public class RegistrationMessage : IMessage
    {
        private readonly string _password;
        private readonly string _loginUrl;
        public string Subject => "Poker Bunch Registration";
        public string Body => string.Format(BodyFormat, _password, _loginUrl);

        public RegistrationMessage(string password, string loginUrl)
        {
            _password = password;
            _loginUrl = loginUrl;
        }

        private const string BodyFormat =
@"Thanks for registering with Poker Bunch.

Here is your password:
{0}

Please sign in here: {1}";
    }
}