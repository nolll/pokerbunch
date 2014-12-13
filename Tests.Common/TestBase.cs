using Core.Entities;
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
        
        public TestBase()
        {
            Mock = new MockContainer();
            A = new BuilderContainer();
            Repos = new RepositoryContainer();
            Services = new ServiceContainer();
        }

        [SetUp]
        public void ClearFakes()
        {
            Mock.Clear();
            Services.Clear();
        }

        protected Mock<T> GetMock<T>() where T : class
        {
            return Mock.Get<T>();
        }
    }
}
