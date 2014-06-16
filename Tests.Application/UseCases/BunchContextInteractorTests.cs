using System.Collections.Generic;
using Application.Services;
using Application.UseCases.ApplicationContext;
using Application.UseCases.BunchContext;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class BunchContextInteractorTests : MockContainer
    {
        [Test]
        public void ExecuteWithSlug_SlugIsSetFromSelectedHomegame()
        {
            const string slug = "a";
            var request = new BunchContextRequest { Slug = slug };
            var applicationContextResult = new ApplicationContextResultInTest();
            var homegame = new HomegameInTest(slug: slug);

            GetMock<IApplicationContextInteractor>().Setup(o => o.Execute()).Returns(applicationContextResult);
            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);

            var result = Sut.Execute(request);

            Assert.AreEqual(slug, result.Slug);
        }

        [Test]
        public void Execute_WithoutSlug_SlugIsSetFromFirstHomegame()
        {
            const string slug = "a";
            var request = new BunchContextRequest();
            var applicationContextResult = new ApplicationContextResultInTest();
            var homegame = new HomegameInTest(slug: slug);

            GetMock<IApplicationContextInteractor>().Setup(o => o.Execute()).Returns(applicationContextResult);
            GetMock<IHomegameRepository>().Setup(o => o.GetByUser(It.IsAny<User>())).Returns(new List<Homegame>{homegame});

            var result = Sut.Execute(request);

            Assert.AreEqual(slug, result.Slug);
        }

        private BunchContextInteractor Sut
        {
            get
            {
                return new BunchContextInteractor(
                    GetMock<IApplicationContextInteractor>().Object,
                    GetMock<IHomegameRepository>().Object,
                    GetMock<IAuth>().Object);
            }
        }
    }
}