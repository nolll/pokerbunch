using Application.UseCases.BaseContext;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.UseCases
{
    class BaseContextInteractorTests : MockContainer
    {
        [Test]
        public void Execute_AllPropertiesAreSet()
        {
            var result = Sut.Execute();

            Assert.IsFalse(result.IsInProduction);
            Assert.IsNotEmpty(result.Version);
        }

        private BaseContextInteractor Sut
        {
            get {
                return new BaseContextInteractor();
            }
        }
    }
}