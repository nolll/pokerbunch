using System.Collections.Generic;
using Application.Services;
using Application.UseCases.ApplicationContext;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.HomeModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UrlModels;

namespace Tests.Web.ModelFactoryTests.HomeModelFactories
{
    public class HomePageModelFactoryTests : MockContainer
    {
        [Test]
        public void AllProperties_DefaultState_IsFalse()
        {
            const string addHomegameUrl = "c";
            GetMock<IUrlProvider>().Setup(o => o.GetHomegameAddUrl()).Returns(addHomegameUrl);

            GetMock<IHomegameRepository>().Setup(o => o.GetByUser(It.IsAny<User>())).Returns(new List<Homegame>());
            GetMock<IApplicationContextInteractor>().Setup(o => o.Execute()).Returns(new ApplicationContextResultInTest());

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsFalse(result.IsLoggedIn);
            Assert.AreEqual(addHomegameUrl, result.AddHomegameUrl);
            Assert.IsInstanceOf<LoginUrlModel>(result.LoginUrl);
            Assert.IsInstanceOf<AddUserUrlModel>(result.RegisterUrl);
        }

        [Test]
        public void IsLoggedIn_WithUser_IsTrue()
        {
            GetMock<IHomegameRepository>().Setup(o => o.GetByUser(It.IsAny<User>())).Returns(new List<Homegame>());
            GetMock<IAuth>().Setup(o => o.IsAuthenticated).Returns(true);
            GetMock<IApplicationContextInteractor>().Setup(o => o.Execute()).Returns(new ApplicationContextResultInTest());

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsTrue(result.IsLoggedIn);
        }

        private HomePageModelFactory GetSut()
        {
            return new HomePageModelFactory(
                GetMock<IAuth>().Object,
                GetMock<IHomegameRepository>().Object,
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<IUrlProvider>().Object,
                GetMock<IApplicationContextInteractor>().Object);
        }
    }
}
