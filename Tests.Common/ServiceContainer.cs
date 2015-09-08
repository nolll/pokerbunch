using Core.Services;
using Tests.Common.FakeServices;

namespace Tests.Common
{
    public class ServiceContainer
    {
        public FakeMessageSender MessageSender { get; private set; }
        public FakeRandomService RandomService { get; private set; }
        public FakeCache Cache { get; private set; }
        public UserService UserService { get; private set; }

        public ServiceContainer(RepositoryContainer repos)
        {
            MessageSender = new FakeMessageSender();
            RandomService = new FakeRandomService();
            Cache = new FakeCache();
            UserService = new UserService(repos.User);
        }

        public void Clear()
        {
            MessageSender.Reset();
        }
    }
}