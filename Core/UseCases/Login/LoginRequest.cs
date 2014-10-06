namespace Core.UseCases.Login
{
    public class LoginRequest
    {
        public string LoginName { get; private set; }
        public string Password { get; private set; }
        public bool RememberMe { get; private set; }
        public string ReturnUrl { get; private set; }

        public LoginRequest(string loginName, string password, bool rememberMe, string returnUrl)
        {
            LoginName = loginName;
            Password = password;
            RememberMe = rememberMe;
            ReturnUrl = returnUrl;
        }
    }
}