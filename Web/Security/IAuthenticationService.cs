namespace Web.Security
{
    public interface IAuthenticationService
    {
        void SignIn(UserIdentity user, bool createPersistentCookie);
        void SignOut();
    }
}