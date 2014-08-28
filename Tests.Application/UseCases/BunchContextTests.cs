using System.Collections.Generic;
using Application.Services;
using Application.UseCases.AppContext;
using Application.UseCases.BunchContext;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class BunchContextTests : MockContainer
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
            var homegameList = AHomegameList.WithOneItem().Build();
            SetupHomegameListByUser(homegameList);

            var result = GetResult();

            Assert.AreEqual("a", result.Slug);
            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void BunchContext_WithoutSlugAndHomegames_SlugIsNull()
        {
            var homegameList = AHomegameList.Build(); 
            SetupHomegameListByUser(homegameList);

            var result = GetResult();

            Assert.IsNull(result.Slug);
            Assert.IsFalse(result.HasBunch);
        }

        [Test]
        public void Execute_AppContextIsSet()
        {
            const string slug = "a";
            var contextResult = new AppContextResultInTest();
            var cashgameContextRequest = new BunchContextRequest(slug);

            GetMock<IAppContextInteractor>().Setup(o => o.Execute()).Returns(contextResult);
            
            var result = Sut.Execute(cashgameContextRequest);

            Assert.IsInstanceOf<AppContextResult>(result.AppContext);
        }

        private BunchContextResult GetResult(string slug = null)
        {
            var request = new BunchContextRequest(slug);
            return Sut.Execute(request);
        }

        private void SetupHomegameBySlug(string slug)
        {
            SetupAppContext();
            var homegame = AHomegame.Build();
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
        }

        private void SetupHomegameListByUser(IList<Bunch> homegameList)
        {
            SetupAppContext();
            GetMock<IBunchRepository>().Setup(o => o.GetByUser(It.IsAny<User>())).Returns(homegameList);
        }

        private void SetupAppContext()
        {
            GetMock<IAppContextInteractor>().Setup(o => o.Execute()).Returns(new AppContextResultInTest());
        }

        private BunchContextInteractor Sut
        {
            get
            {
                return new BunchContextInteractor(
                    GetMock<IAppContextInteractor>().Object,
                    GetMock<IBunchRepository>().Object,
                    GetMock<IAuth>().Object);
            }
        }
    }
}