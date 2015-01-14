using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases.AppContext;
using Core.UseCases.BunchContext;
using Moq;
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
            const string slug = Constants.SlugA;

            var result = GetResult2(slug);

            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_WithoutSlug_HasBunchIsTrue()
        {
            var homegameList = A.BunchList.WithOneItem().Build();
            SetupHomegameListByUser(homegameList);

            var result = GetResult();

            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_WithoutSlugAndBunches_HasBunchIsFalse()
        {
            var homegameList = A.BunchList.Build(); 
            SetupHomegameListByUser(homegameList);

            var result = GetResult();

            Assert.IsFalse(result.HasBunch);
        }

        [Test]
        public void Execute_AppContextIsSet()
        {
            const string slug = "a";
            var cashgameContextRequest = new BunchContextRequest(slug);

            var result = Execute(cashgameContextRequest);

            Assert.IsInstanceOf<AppContextResult>(result.AppContext);
        }

        private BunchContextResult Execute(BunchContextRequest request)
        {
            return BunchContextInteractor.Execute(
                AppContextFunc,
                GetMock<IBunchRepository>().Object,
                Services.Auth,
                request);
        }

        private BunchContextResult Execute2(BunchContextRequest request)
        {
            return BunchContextInteractor.Execute(
                AppContextFunc,
                Repos.Bunch,
                Services.Auth,
                request);
        }

        private AppContextResult AppContextFunc()
        {
            return new AppContextResultInTest();
        }

        private BunchContextResult GetResult(string slug = null)
        {
            return Execute(new BunchContextRequest(slug));
        }

        private BunchContextResult GetResult2(string slug = null)
        {
            return Execute2(new BunchContextRequest(slug));
        }

        private void SetupHomegameListByUser(IList<Bunch> homegameList)
        {
            GetMock<IBunchRepository>().Setup(o => o.GetByUserId(It.IsAny<int>())).Returns(homegameList);
        }
    }
}