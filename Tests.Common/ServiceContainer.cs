using Tests.Common.FakeServices;

namespace Tests.Common
{
    public class ServiceContainer
    {
        public FakeMessageSender MessageSender { get; private set; }
        public FakeRandomService RandomService { get; private set; }
        public FakeCache Cache { get; private set; }

        public ServiceContainer()
        {
            MessageSender = new FakeMessageSender();
            RandomService = new FakeRandomService();
            Cache = new FakeCache();
        }

        public void Clear()
        {
            MessageSender.Reset();
        }
    }
}