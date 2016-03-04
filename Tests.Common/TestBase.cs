using NUnit.Framework;

namespace Tests.Common
{
    public class TestBase
    {
        protected Mocker Mocker { get; private set; }
        protected RepositoryContainer Repos { get; private set; }
        protected ServiceContainer Services { get; private set; }
        
        [SetUp]
        public void ClearFakes()
        {
            Mocker = new Mocker();
            Repos = new RepositoryContainer();
            Services = new ServiceContainer(Repos);
        }
    }
}
