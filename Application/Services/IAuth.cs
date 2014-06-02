using Core.Entities;

namespace Application.Services
{
    public interface IAuth
    {
        void SignIn(UserIdentity user, bool createPersistentCookie);
        void SignOut();
        User CurrentUser { get; }
        bool IsInRole(string slug, Role manager);
        Role GetRole(string slug);
        bool IsAdmin { get; }
        bool IsAuthenticated { get; }
    }
}