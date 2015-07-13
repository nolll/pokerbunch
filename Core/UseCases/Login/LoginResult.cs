namespace Core.UseCases.Login
{
    public class LoginResult
    {
        public string UserName { get; private set; }

        public LoginResult(string userName)
        {
            UserName = userName;
        }
    }
}