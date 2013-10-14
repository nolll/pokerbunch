using System.Collections.Generic;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.HomeModelFactories;

namespace Web.Tests.ModelFactoryTests.HomeModelFactories
{
    public class HomePageModelFactoryTests : WebMockContainer
    {
        [Test]
        public void AllProperties_DefaultState_IsFalse()
        {
            Mocks.HomegameRepositoryMock.Setup(o => o.GetByUser(It.IsAny<User>()))
                                  .Returns(new List<Homegame>());

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsFalse(result.IsLoggedIn);
            Assert.IsNotNull(result.AddHomegameUrl);
            Assert.IsNotNull(result.LoginUrl);
            Assert.IsNotNull(result.RegisterUrl);
        }

        [Test]
        public void IsLoggedIn_WithUser_IsTrue()
        {
            Mocks.HomegameRepositoryMock.Setup(o => o.GetByUser(It.IsAny<User>()))
                                  .Returns(new List<Homegame>());
            Mocks.UserContextMock.Setup(o => o.GetUser()).Returns(new User());

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsTrue(result.IsLoggedIn);
        }

        private HomePageModelFactory GetSut()
        {
            return new HomePageModelFactory
                (
                Mocks.UserContextMock.Object,
                Mocks.HomegameRepositoryMock.Object,
                Mocks.CashgameRepositoryMock.Object,
                Mocks.PagePropertiesFactoryMock.Object,
                Mocks.AdminNavigationModelFactoryMock.Object
                );
        }
    }
}
