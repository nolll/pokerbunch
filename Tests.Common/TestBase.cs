using Moq;
using NUnit.Framework;

namespace Tests.Common
{
    public class TestBase
    {
        private MockContainer Mock { get; set; }
        protected BuilderContainer A { get; private set; }
        protected RepositoryContainer Repo { get; private set; }
        
        public TestBase()
        {
            Mock = new MockContainer();
            A = new BuilderContainer();
            Repo = new RepositoryContainer();
        }

        [SetUp]
        public void SetUpMocks()
        {
            Mock.Clear();
        }

        protected Mock<T> GetMock<T>() where T : class
        {
            return Mock.Get<T>();
        }
    }
}
