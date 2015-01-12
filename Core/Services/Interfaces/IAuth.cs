using Core.Entities;

namespace Core.Services
{
    public interface IAuth
    {
        void SignIn(UserIdentity user, bool createPersistentCookie);
        void SignOut();
        CustomIdentity CurrentIdentity { get; }
        bool IsInRole(string slug, Role manager);
    }
}