namespace Core.Services
{
    public interface IUserService
    {
        bool IsUserNameAvailable(string userName);
        bool IsEmailAvailable(string email);
    }
}
