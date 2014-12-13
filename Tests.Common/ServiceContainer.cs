using Tests.Common.FakeServices;

namespace Tests.Common
{
    public class ServiceContainer
    {
        public FakeTimeProvider Time { get; private set; }
        public FakeAuth Auth { get; private set; }
        public FakeMessageSender MessageSender { get; private set; }

        public ServiceContainer()
        {
            Time = new FakeTimeProvider();
            Auth = new FakeAuth();
            MessageSender = new FakeMessageSender();
        }

        public void Clear()
        {
            Time.Reset();
            Auth.Reset();
            MessageSender.Reset();
        }
    }
}