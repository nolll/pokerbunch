namespace Core.Entities
{
    public class AuthToken
    {
        public string UserName { get; }
        public string Token { get; }

        public AuthToken(string userName, string token)
        {
            UserName = userName;
            Token = token;
        }
    }
}