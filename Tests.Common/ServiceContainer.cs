using Core.Services;
using Tests.Common.FakeServices;

namespace Tests.Common
{
    public class ServiceContainer
    {
        public FakeMessageSender MessageSender { get; private set; }
        public FakeRandomService RandomService { get; private set; }
        public BunchService BunchService { get; private set; }
        public CashgameService CashgameService { get; private set; }
        public UserService UserService { get; private set; }
        public EventService EventService { get; private set; }
        public PlayerService PlayerService { get; private set; }
        public LocationService LocationService { get; private set; }

        public ServiceContainer(RepositoryContainer repos)
        {
            MessageSender = new FakeMessageSender();
            RandomService = new FakeRandomService();
            BunchService = new BunchService(repos.Bunch);
            CashgameService = new CashgameService(repos.Cashgame);
            UserService = new UserService(repos.User);
            EventService = new EventService(repos.Event);
            PlayerService = new PlayerService(repos.Player);
            LocationService = new LocationService(repos.Location);
        }

        public void Clear()
        {
            MessageSender.Reset();
        }
    }
}