using Moq;
using NUnit.Framework;

namespace Tests.Core
{
    public abstract class UseCaseTest<T> where T : class
    {
        private DependencyMocker _dependencyMocker;
        protected T Sut { get; private set; }

        [SetUp]
        public void TestBaseSetup()
        {
            _dependencyMocker = new DependencyMocker();
            Sut = _dependencyMocker.New<T>();
        }

        protected Mock<TM> Mock<TM>() where TM : class => _dependencyMocker.MockOf<TM>();
    }
}