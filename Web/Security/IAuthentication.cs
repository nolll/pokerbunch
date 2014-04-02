using Core.Classes;

namespace Web.Security
{
    public interface IAuthentication
    {
        void SignIn(UserIdentity user, bool createPersistentCookie);
        void SignOut();
        User GetUser();
        bool IsInRole(string slug, Role manager);
        Role GetRole(string slug);
    }
}