using Tests.Common.FakeServices;

namespace Tests.Common
{
    public class ServiceContainer
    {
        public FakeTimeProvider Time { get; set; }

        public ServiceContainer()
        {
            Time = new FakeTimeProvider();
        }
    }
}