using System.Collections.Generic;
using Application.Services;
using Application.UseCases.AppContext;
using Application.UseCases.BunchContext;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.Builders;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class BunchContextTests : MockContainer
    {
        [Test]
        public void ExecuteWithSlug_SlugIsSetFromSelectedHomegame()
        {
            const string slug = "a";
            var request = new BunchContextRequest(slug);
            var contextResult = new AppContextResultInTest();
            var homegame = new HomegameBuilder().Build();

            GetMock<IAppContextInteractor>().Setup(o => o.Execute()).Returns(contextResult);
            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(It.IsAny<string>())).Returns(homegame);

            var result = Sut.Execute(request);

            Assert.AreEqual(slug, result.Slug);
            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void Execute_WithoutSlug_SlugIsSetFromFirstHomegame()
        {
            var request = new BunchContextRequest();
            var contextResult = new AppContextResultInTest();
            var homegameList = new HomegameListBuilder().WithOneItem().Build();

            GetMock<IAppContextInteractor>().Setup(o => o.Execute()).Returns(contextResult);
            GetMock<IHomegameRepository>().Setup(o => o.GetByUser(It.IsAny<User>())).Returns(homegameList);

            var result = Sut.Execute(request);

            Assert.AreEqual("a", result.Slug);
            Assert.IsTrue(result.HasBunch);
        }

        [Test]
        public void Execute_WithoutHomegame_SlugIsNull()
        {
            var request = new BunchContextRequest();
            var contextResult = new AppContextResultInTest();
            var homegameList = new HomegameListBuilder().Build();

            GetMock<IAppContextInteractor>().Setup(o => o.Execute()).Returns(contextResult);
            GetMock<IHomegameRepository>().Setup(o => o.GetByUser(It.IsAny<User>())).Returns(homegameList);

            var result = Sut.Execute(request);

            Assert.IsNull(result.Slug);
            Assert.IsFalse(result.HasBunch);
        }

        private BunchContextInteractor Sut
        {
            get
            {
                return new BunchContextInteractor(
                    GetMock<IAppContextInteractor>().Object,
                    GetMock<IHomegameRepository>().Object,
                    GetMock<IAuth>().Object);
            }
        }
    }
}