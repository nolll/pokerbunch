using Moq;
using NUnit.Framework;

namespace Tests.Common
{
    public class TestBase
    {
        private MockContainer Mock { get; set; }
        protected BuilderContainer A { get; private set; }
        protected RepositoryContainer Repos { get; private set; }
        protected ServiceContainer Services { get; private set; }
        
        [SetUp]
        public void ClearFakes()
        {
            Mock = new MockContainer();
            A = new BuilderContainer();
            Repos = new RepositoryContainer();
            Services = new ServiceContainer();
        }

        protected Mock<T> GetMock<T>() where T : class
        {
            return Mock.Get<T>();
        }
    }
}
