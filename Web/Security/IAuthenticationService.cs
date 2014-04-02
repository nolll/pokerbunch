using Core.Classes;

namespace Web.Security
{
    public interface IAuthenticationService
    {
        void SignIn(UserIdentity user, bool createPersistentCookie);
        void SignOut();
        CustomIdentity GetIdentity();
        User GetUser();
        bool IsInRole(string slug, Role manager);
        Role GetRole(string slug);
    }
}