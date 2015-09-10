namespace Core.Repositories
{
    public interface IRepositoryFactory
    {
        IAppRepository GetAppRepository(IAppRepository appRepository);
        IBunchRepository GetBunchRepository(IBunchRepository bunchRepository);
        ICashgameRepository GetCashgameRepository(ICashgameRepository cashgameService);
        ICheckpointRepository GetCheckpointRepository(ICheckpointRepository checkpointRepository);
        IEventRepository GetEventRepository(IEventRepository eventRepository);
        IPlayerRepository GetPlayerRepository(IPlayerRepository playerRepository);
        IUserRepository GetUserRepository(IUserRepository userRepository);
    }
}