using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.NavigationModels;

namespace Web.Tests.ModelFactoryTests.BaseModelFactories
{
    public class PagePropertiesFactoryTests : MockContainer
    {
        [Test]
        public void Create_WithoutHomeGame_HomegameNavModelIsNull()
        {
            var user = new User();
            HomegameNavigationModelFactoryMock.Setup(o => o.Create(It.IsAny<Homegame>(), It.IsAny<Cashgame>())).Returns(new HomegameNavigationModel());

            var sut = GetSut();
            var result = sut.Create(user);

            Assert.IsNull(result.HomegameNavModel);
        }

        [Test]
        public void HomegameNavModel_WithOneHomeGame_IsNotNull()
        {
            var user = new User();
            var homegame = new Homegame();
            HomegameNavigationModelFactoryMock.Setup(o => o.Create(It.IsAny<Homegame>(), It.IsAny<Cashgame>())).Returns(new HomegameNavigationModel());

            var sut = GetSut();
            var result = sut.Create(user, homegame);

            Assert.IsNotNull(result.HomegameNavModel);
        }

        private PagePropertiesFactory GetSut()
        {
            return new PagePropertiesFactory(
                GoogleAnalyticsModelFactoryMock.Object,
                HomegameNavigationModelFactoryMock.Object);
        }

    }
}
