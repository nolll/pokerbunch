using Core.Urls;

namespace Core.Services
{
    public class RegistrationMessage : IMessage
    {
        private readonly string _password;

        public RegistrationMessage(string password)
        {
            _password = password;
        }

        public string Subject
        {
            get { return "Poker Bunch Registration"; }
        }

        public string Body
        {
            get
            {
                var loginUrl = new LoginUrl().Absolute;
                return string.Format(BodyFormat, _password, loginUrl);
            }
        }

        private const string BodyFormat =
@"Thanks for registering with Poker Bunch.

Here is your password:
{0}

Please sign in here: {1}";
    }
}