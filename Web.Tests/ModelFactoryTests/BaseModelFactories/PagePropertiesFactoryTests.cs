using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.NavigationModels;

namespace Web.Tests.ModelFactoryTests.BaseModelFactories
{
    public class PagePropertiesFactoryTests : WebMockContainer
    {
        [Test]
        public void Create_WithoutHomeGame_HomegameNavModelIsNull()
        {
            var user = new FakeUser();
            Mocks.HomegameNavigationModelFactoryMock.Setup(o => o.Create(It.IsAny<Homegame>())).Returns(new HomegameNavigationModel());

            var sut = GetSut();
            var result = sut.Create(user);

            Assert.IsNull(result.HomegameNavModel);
        }

        [Test]
        public void HomegameNavModel_WithOneHomeGame_IsNotNull()
        {
            var user = new FakeUser();
            var homegame = new FakeHomegame();
            Mocks.HomegameNavigationModelFactoryMock.Setup(o => o.Create(It.IsAny<Homegame>())).Returns(new HomegameNavigationModel());

            var sut = GetSut();
            var result = sut.Create(user, homegame);

            Assert.IsNotNull(result.HomegameNavModel);
        }

        private PagePropertiesFactory GetSut()
        {
            return new PagePropertiesFactory(
                Mocks.GoogleAnalyticsModelFactoryMock.Object,
                Mocks.HomegameNavigationModelFactoryMock.Object,
                Mocks.UserNavigationModelFactoryMock.Object);
        }

    }
}
