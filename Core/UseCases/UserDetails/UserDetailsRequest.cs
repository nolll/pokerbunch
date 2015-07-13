namespace Core.UseCases.UserDetails
{
    public class UserDetailsRequest
    {
        public string CurrentUserName { get; private set; }
        public string UserName { get; private set; }

        public UserDetailsRequest(string currentUserName, string userName)
        {
            CurrentUserName = currentUserName;
            UserName = userName;
        }
    }
}