using NUnit.Framework;

namespace Infrastructure.Tests
{
    public class InfrastructureMockContainer
    {
        protected InfrastructureMocks Mocks;

        [SetUp]
        public void SetUpMocks()
        {
            Mocks = new InfrastructureMocks();
        }

    }

}
