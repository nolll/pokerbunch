using Core.UseCases;
using NUnit.Framework;
using Tests.Common;
using Tests.Core.Data;

namespace Tests.Core.UseCases
{
    public class BunchContextTests : TestBase
    {
        [Test]
        public void BunchContext_WithSlug_HasBunchIsTrue()
        {
            var result = Sut.Execute(CoreContext, new BunchContext.BunchRequest(TestData.SlugA));

            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_OneBunchWithoutSlug_HasBunchIsTrue()
        {
            Deps.Bunch.SetupOneBunchList();

            var result = Sut.Execute(CoreContext, new BunchContext.BunchRequest());

            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_WithoutSlugAndBunches_HasBunchIsFalse()
        {
            Deps.Bunch.ClearList();

            var result = Sut.Execute(CoreContext, new BunchContext.BunchRequest());

            Assert.IsFalse(result.HasBunch);
        }

        [Test]
        public void Execute_AppContextIsSet()
        {
            var request = new BunchContext.BunchRequest(TestData.SlugA);

            var result = Sut.Execute(CoreContext, request);

            Assert.IsInstanceOf<CoreContext.Result>(result.AppContext);
        }

        private static BaseContext.Result BaseContext => new BaseContext.Result("1");
        private static CoreContext.Result CoreContext => new CoreContext.Result(BaseContext, true, false, UserData.UserName1, UserData.DisplayName1);
        private BunchContext Sut => new BunchContext(Deps.Bunch);
    }
}