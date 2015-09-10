using Core.Services;
using Tests.Common.FakeServices;

namespace Tests.Common
{
    public class ServiceContainer
    {
        public FakeMessageSender MessageSender { get; private set; }
        public FakeRandomService RandomService { get; private set; }
        public FakeCache Cache { get; private set; }
        public BunchService BunchService { get; private set; }
        public CashgameService CashgameService { get; private set; }
        public UserService UserService { get; private set; }
        public EventService EventService { get; set; }

        public ServiceContainer(RepositoryContainer repos)
        {
            MessageSender = new FakeMessageSender();
            RandomService = new FakeRandomService();
            Cache = new FakeCache();
            BunchService = new BunchService(repos.Bunch);
            CashgameService = new CashgameService(repos.Cashgame);
            UserService = new UserService(repos.User);
            EventService = new EventService(repos.Event);
        }

        public void Clear()
        {
            MessageSender.Reset();
        }
    }
}