using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.NavigationModels;
using Web.Security;

namespace Tests.Web.ModelFactoryTests.BaseModelFactories
{
    public class PagePropertiesFactoryTests : MockContainer
    {
        [Test]
        public void Create_WithoutHomeGame_HomegameNavModelIsNull()
        {
            GetMock<IHomegameNavigationModelFactory>().Setup(o => o.Create(It.IsAny<Homegame>())).Returns(new HomegameNavigationModel());

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsNull(result.HomegameNavModel);
        }

        [Test]
        public void HomegameNavModel_WithOneHomeGame_IsNotNull()
        {
            var homegame = new FakeHomegame();
            GetMock<IHomegameNavigationModelFactory>().Setup(o => o.Create(It.IsAny<Homegame>())).Returns(new HomegameNavigationModel());

            var sut = GetSut();
            var result = sut.Create(homegame);

            Assert.IsNotNull(result.HomegameNavModel);
        }

        private PagePropertiesFactory GetSut()
        {
            return new PagePropertiesFactory(
                GetMock<IGoogleAnalyticsModelFactory>().Object,
                GetMock<IHomegameNavigationModelFactory>().Object,
                GetMock<IUserNavigationModelFactory>().Object,
                GetMock<IAuthentication>().Object);
        }

    }
}
