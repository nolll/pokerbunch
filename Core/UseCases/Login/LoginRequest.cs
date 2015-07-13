namespace Core.UseCases.Login
{
    public class LoginRequest
    {
        public string LoginName { get; private set; }
        public string Password { get; private set; }

        public LoginRequest(string loginName, string password)
        {
            LoginName = loginName;
            Password = password;
        }
    }
}