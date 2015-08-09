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
            var result = Sut.Execute();

            Assert.IsNotEmpty(result.Version);
        }

        [Test]
        public void BaseContext_AnyServerName_IsInProductionIsFalse()
        {
            var result = Sut.Execute();
            
            Assert.IsFalse(result.IsInProduction);
        }

        private BaseContext Sut
        {
            get { return new BaseContext(); }
        }
    }
}