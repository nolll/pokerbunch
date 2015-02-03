using Tests.Common.FakeServices;

namespace Tests.Common
{
    public class ServiceContainer
    {
        public FakeAuth Auth { get; private set; }
        public FakeMessageSender MessageSender { get; private set; }
        public FakeRandomService RandomService { get; private set; }
        public FakeCache Cache { get; private set; }

        public ServiceContainer()
        {
            Auth = new FakeAuth();
            MessageSender = new FakeMessageSender();
            RandomService = new FakeRandomService();
            Cache = new FakeCache();
        }

        public void Clear()
        {
            Auth.Reset();
            MessageSender.Reset();
        }
    }
}