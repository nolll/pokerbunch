using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class ClearCacheTests : TestBase
    {
        [Test]
        public void ClearCache_ClearsCacheAndReturnsNumberOfClearedObjects()
        {
            var result = Sut.Execute();
            Assert.AreEqual(1, result.DeleteCount);
        }

        private ClearCache Sut
        {
            get
            {
                return new ClearCache(Services.Cache);
            }
        }
    }
}