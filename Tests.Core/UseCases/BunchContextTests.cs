using Core.UseCases.AppContext;
using Core.UseCases.BunchContext;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Core.UseCases
{
    class BunchContextTests : TestBase
    {
        [Test]
        public void BunchContext_WithSlug_HasBunchIsTrue()
        {
            var result = Execute(new BunchContextRequest(Constants.SlugA));

            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_OneBunchWithoutSlug_HasBunchIsTrue()
        {
            Repos.Bunch.SetupOneBunchList();

            var result = Execute(new BunchContextRequest());

            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_WithoutSlugAndBunches_HasBunchIsFalse()
        {
            Repos.Bunch.ClearList();

            var result = Execute(new BunchContextRequest(Constants.SlugA));

            Assert.IsFalse(result.HasBunch);
        }

        [Test]
        public void Execute_AppContextIsSet()
        {
            var cashgameContextRequest = new BunchContextRequest(Constants.SlugA);

            var result = Execute(cashgameContextRequest);

            Assert.IsInstanceOf<AppContextResult>(result.AppContext);
        }

        private BunchContextResult Execute(BunchContextRequest request)
        {
            return BunchContextInteractor.Execute(
                new AppContextResultInTest(), 
                Repos.Bunch,
                Services.Auth,
                request);
        }
    }
}