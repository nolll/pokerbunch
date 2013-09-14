using System.Collections.Generic;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.HomeModelFactories;

namespace Web.Tests.ModelFactories
{
    class HomePageModelFactoryTests : MockContainer
    {
        [Test]
        public void AllProperties_DefaultState_IsFalse()
        {
            HomegameRepositoryMock.Setup(o => o.GetByUser(It.IsAny<User>()))
                               .Returns(new List<Homegame>());

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsFalse(result.IsLoggedIn);
            Assert.IsNotNull(result.AddHomegameUrl);
            Assert.IsNotNull(result.LoginUrl);
            Assert.IsNotNull(result.RegisterUrl);
            Assert.IsNotNull(result.AdminNav);
        }

        [Test]
        public void IsLoggedIn_WithUser_IsTrue()
        {
            HomegameRepositoryMock.Setup(o => o.GetByUser(It.IsAny<User>()))
                               .Returns(new List<Homegame>());
            UserContextMock.Setup(o => o.GetUser()).Returns(new User());

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsTrue(result.IsLoggedIn);
        }

        [Test]
        public void HomegameNavModel_WithoutHomeGame_IsNull()
        {
            HomegameRepositoryMock.Setup(o => o.GetByUser(It.IsAny<User>()))
                               .Returns(new List<Homegame>());

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsNull(result.PageProperties.HomegameNavModel);
        }

        [Test]
        public void HomegameNavModel_WithTwoHomeGames_IsNull()
        {
            HomegameRepositoryMock.Setup(o => o.GetByUser(It.IsAny<User>()))
                               .Returns(new List<Homegame>{new Homegame(), new Homegame()});

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsNull(result.PageProperties.HomegameNavModel);
        }

        [Test]
        public void HomegameNavModel_WithOneHomeGame_IsNotNull()
        {
            HomegameRepositoryMock.Setup(o => o.GetByUser(It.IsAny<User>()))
                               .Returns(new List<Homegame> { new Homegame() });

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsNotNull(result.PageProperties.HomegameNavModel);
        }

        private HomePageModelFactory GetSut()
        {
            return new HomePageModelFactory(UserContextMock.Object, HomegameRepositoryMock.Object, CashgameRepositoryMock.Object);
        }
    }
}
