using Core.UseCases.AppContext;
using Core.UseCases.BunchContext;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class BunchContextTests : TestBase
    {
        [Test]
        public void BunchContext_WithSlug_HasBunchIsTrue()
        {
            var result = Sut.Execute(new BunchContextRequest(TestData.UserA.UserName, TestData.SlugA));

            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_OneBunchWithoutSlug_HasBunchIsTrue()
        {
            Repos.Bunch.SetupOneBunchList();

            var result = Sut.Execute(new BunchContextRequest(TestData.UserA.UserName));

            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_WithoutSlugAndBunches_HasBunchIsFalse()
        {
            Repos.Bunch.ClearList();

            var result = Sut.Execute(new BunchContextRequest(TestData.UserA.UserName));

            Assert.IsFalse(result.HasBunch);
        }

        [Test]
        public void Execute_AppContextIsSet()
        {
            var cashgameContextRequest = new BunchContextRequest(TestData.UserA.UserName, TestData.SlugA);

            var result = Sut.Execute(cashgameContextRequest);

            Assert.IsInstanceOf<AppContextResult>(result.AppContext);
        }

        private BunchContextInteractor Sut
        {
            get
            {
                return new BunchContextInteractor(
                    Repos.User,
                    Repos.Bunch,
                    Repos.Player);
            }
        }
    }
}