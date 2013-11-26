using Core.Classes;

namespace Infrastructure.Caching
{
    public interface ICacheBuster
    {
        void UserAdded();
        void UserUpdated(User user);
    }
}