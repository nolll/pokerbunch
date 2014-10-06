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

namespace Tests.Application.UseCases
{
    class BunchContextTests : TestBase
    {
        [Test]
        public void BunchContext_WithSlug_SlugIsSetFromSelectedHomegame()
        {
            const string slug = "a";
            SetupHomegameBySlug(slug);

            var result = GetResult(slug);

            Assert.AreEqual(slug, result.Slug);
            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_WithoutSlug_SlugIsSetFromFirstHomegame()
        {
            var homegameList = A.BunchList.WithOneItem().Build();
            SetupHomegameListByUser(homegameList);

            var result = GetResult();

            Assert.AreEqual("a", result.Slug);
            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_WithoutSlugAndHomegames_SlugIsNull()
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
            var homegame = A.Bunch.Build();
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
        }

        private void SetupHomegameListByUser(IList<Bunch> homegameList)
        {
            GetMock<IBunchRepository>().Setup(o => o.GetByUser(It.IsAny<User>())).Returns(homegameList);
        }
    }
}