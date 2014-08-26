using Application.Urls;

namespace Application.Services
{
    public static class PasswordMessageBuilder
    {
        public static string GetSubject()
        {
            return "Poker Bunch password recovery";
        }

        public static string GetBody(string password)
        {
            var loginUrl = new LoginUrl().Absolute;
            return string.Format(BodyFormat, password, loginUrl);
        }

        private const string BodyFormat =
@"Here is your new password for Poker Bunch:
{0}

Please sign in here: {1}";

    }
}