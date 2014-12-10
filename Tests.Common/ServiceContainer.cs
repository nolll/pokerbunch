using Tests.Common.FakeServices;

namespace Tests.Common
{
    public class ServiceContainer
    {
        public FakeTimeProvider Time { get; private set; }
        public FakeAuth Auth { get; private set; }

        public ServiceContainer()
        {
            Time = new FakeTimeProvider();
            Auth = new FakeAuth();
        }
    }
}