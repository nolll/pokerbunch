using Application.Urls;

namespace Application.Services
{
    public static class RegistrationConfirmationMessageBuilder
    {
        public static string GetSubject()
        {
            return "Poker Bunch Registration";
        }

        public static string GetBody(string password)
        {
            var loginUrl = new LoginUrl().Absolute;
            return string.Format(BodyFormat, password, loginUrl);
        }

        private const string BodyFormat = 
@"Thanks for registering with Poker Bunch.

Here is your password:
{0}

Please sign in here: {1}";
    }
}