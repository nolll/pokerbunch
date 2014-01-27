namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        bool IsUserNameAvailable(string userName);
        bool IsEmailAvailable(string email);
    }
}
