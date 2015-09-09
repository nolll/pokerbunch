namespace Core.Repositories
{
    public interface IRepositoryFactory
    {
        IUserRepository GetUserRepository(IUserRepository userRepository);
        IBunchRepository GetBunchRepository(IBunchRepository bunchRepository);
    }
}