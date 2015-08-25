namespace Core.Services
{
    public class RegistrationMessage : IMessage
    {
        private readonly string _password;
        private readonly string _loginUrl;

        public RegistrationMessage(string password, string loginUrl)
        {
            _password = password;
            _loginUrl = loginUrl;
        }

        public string Subject
        {
            get { return "Poker Bunch Registration"; }
        }

        public string Body
        {
            get
            {
                return string.Format(BodyFormat, _password, _loginUrl);
            }
        }

        private const string BodyFormat =
@"Thanks for registering with Poker Bunch.

Here is your password:
{0}

Please sign in here: {1}";
    }
}