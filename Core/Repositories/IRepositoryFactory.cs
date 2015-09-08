namespace Core.Repositories
{
    public interface IRepositoryFactory
    {
        IUserRepository GetUserRepository(IUserRepository userRepository);
    }
}