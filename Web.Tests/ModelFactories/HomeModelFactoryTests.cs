using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories;

namespace Web.Tests.ModelFactories
{
    class HomeModelFactoryTests : MockContainer
    {
        [Test]
        public void AllProperties_DefaultState_IsFalse()
        {
            HomegameStorageMock.Setup(o => o.GetHomegamesByRole(It.IsAny<string>(), It.IsAny<int>()))
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
            HomegameStorageMock.Setup(o => o.GetHomegamesByRole(It.IsAny<string>(), It.IsAny<int>()))
                               .Returns(new List<Homegame>());
            UserContextMock.Setup(o => o.GetUser()).Returns(new User());

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsTrue(result.IsLoggedIn);
        }

        [Test]
        public void HomegameNavModel_WithoutHomeGame_IsNull()
        {
            HomegameStorageMock.Setup(o => o.GetHomegamesByRole(It.IsAny<string>(), It.IsAny<int>()))
                               .Returns(new List<Homegame>());

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsNull(result.HomegameNavModel);
        }

        [Test]
        public void HomegameNavModel_WithTwoHomeGames_IsNull()
        {
            HomegameStorageMock.Setup(o => o.GetHomegamesByRole(It.IsAny<string>(), It.IsAny<int>()))
                               .Returns(new List<Homegame>{new Homegame(), new Homegame()});

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsNull(result.HomegameNavModel);
        }

        [Test]
        public void HomegameNavModel_WithOneHomeGame_IsNotNull()
        {
            HomegameStorageMock.Setup(o => o.GetHomegamesByRole(It.IsAny<string>(), It.IsAny<int>()))
                               .Returns(new List<Homegame> { new Homegame() });

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsNotNull(result.HomegameNavModel);
        }

        private HomeModelFactory GetSut()
        {
            return new HomeModelFactory(UserContextMock.Object, HomegameStorageMock.Object, CashgameRepositoryMock.Object);
        }
    }
}
