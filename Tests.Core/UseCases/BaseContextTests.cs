using Core.UseCases.BaseContext;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class BaseContextTests : TestBase
    {
        [Test]
        public void BaseContext_VersionIsSet()
        {
            var result = Execute();

            Assert.IsNotEmpty(result.Version);
        }

        [Test]
        public void BaseContext_AnyServerName_IsInProductionIsFalse()
        {
            var result = Execute();
            
            Assert.IsFalse(result.IsInProduction);
        }

        private BaseContextResult Execute()
        {
            return BaseContextInteractor.Execute();
        }
    }
}