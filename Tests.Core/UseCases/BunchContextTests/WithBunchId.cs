using Core.UseCases;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.BunchContextTests
{
    public class WithBunchId : Arrange
    {
        protected override string BunchId => BunchData.Id1;

        [Test]
        public void BunchContext_WithSlug_HasBunchIsTrue()
        {
            Assert.IsTrue(Result.HasBunch);
        }

        [Test]
        public void Execute_AppContextIsSet()
        {
            Assert.IsInstanceOf<CoreContext.Result>(Result.AppContext);
        }
    }
}