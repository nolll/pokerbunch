using Application.Urls;

namespace Application.UseCases.ForgotPassword
{
    public class ForgotPasswordMessage : IMessage
    {
        private readonly string _password;

        public ForgotPasswordMessage(string password)
        {
            _password = password;
        }

        public string Subject
        {
            get { return "Poker Bunch Password Recovery"; }
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
@"Here is your new password for Poker Bunch:
{0}

Please sign in here: {1}";
    }
}