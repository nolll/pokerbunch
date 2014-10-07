using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;
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
        public void BunchContext_WithSlug_SlugIsSetFromSelectedBunch()
        {
            const string slug = "a";
            SetupHomegameBySlug(slug);

            var result = GetResult(slug);

            Assert.AreEqual(slug, result.Slug);
            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_WithoutSlug_SlugIsSetFromFirstBunch()
        {
            var homegameList = A.BunchList.WithOneItem().Build();
            SetupHomegameListByUser(homegameList);

            var result = GetResult();

            Assert.AreEqual("a", result.Slug);
            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_WithoutSlugAndBunches_SlugIsNull()
        {
            var homegameList = A.BunchList.Build(); 
            SetupHomegameListByUser(homegameList);

            var result = GetResult();

            Assert.IsNull(result.Slug);
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
            return BunchContextInteractor.Execute(AppContextFunc, GetMock<IBunchRepository>().Object, GetMock<IAuth>().Object, request);
        }

        private AppContextResult AppContextFunc()
        {
            return new AppContextResultInTest();
        }

        private BunchContextResult GetResult(string slug = null)
        {
            var request = new BunchContextRequest(slug);
            return Execute(request);
        }

        private void SetupHomegameBySlug(string slug)
        {
            var bunch = A.Bunch.WithSlug(slug).Build();
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(slug)).Returns(bunch);
        }

        private void SetupHomegameListByUser(IList<Bunch> homegameList)
        {
            GetMock<IBunchRepository>().Setup(o => o.GetByUser(It.IsAny<User>())).Returns(homegameList);
        }
    }
}