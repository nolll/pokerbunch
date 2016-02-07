using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class BaseContextTests : TestBase
    {
        [Test]
        public void BaseContext_VersionIsSet()
        {
            var result = Sut.Execute(new BaseContext.Request(false));

            Assert.IsNotEmpty(result.Version);
        }

        [Test]
        public void BaseContext_InTest_IsInProductionIsFalse()
        {
            var result = Sut.Execute(new BaseContext.Request(false));

            Assert.IsFalse(result.IsInProduction);
        }

        [Test]
        public void BaseContext_InProduction_IsInProductionIsTrue()
        {
            var result = Sut.Execute(new BaseContext.Request(true));

            Assert.IsTrue(result.IsInProduction);
        }

        private BaseContext Sut => new BaseContext();
    }
}