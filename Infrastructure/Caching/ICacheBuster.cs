namespace Infrastructure.Caching
{
    public interface ICacheBuster
    {
        void UserAdded();
        void UserUpdated(int userId);
    }
}