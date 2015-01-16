using NUnit.Framework;

namespace Tests.Common
{
    public class TestBase
    {
        protected BuilderContainer A { get; private set; }
        protected RepositoryContainer Repos { get; private set; }
        protected ServiceContainer Services { get; private set; }
        
        [SetUp]
        public void ClearFakes()
        {
            A = new BuilderContainer();
            Repos = new RepositoryContainer();
            Services = new ServiceContainer();
        }
    }
}
