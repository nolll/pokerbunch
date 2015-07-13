using Core.Entities;

namespace Core.Services
{
    public interface IAuth
    {
        void SignIn(UserIdentity user, bool createPersistentCookie);
        bool IsInRole(string slug, Role manager);
    }
}