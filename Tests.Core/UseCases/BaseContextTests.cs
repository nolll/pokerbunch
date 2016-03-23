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

        private BaseContext Sut => new BaseContext();
    }
}