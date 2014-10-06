namespace Core.UseCases.UserDetails
{
    public class UserDetailsRequest
    {
        public string UserName { get; private set; }

        public UserDetailsRequest(string userName)
        {
            UserName = userName;
        }
    }
}