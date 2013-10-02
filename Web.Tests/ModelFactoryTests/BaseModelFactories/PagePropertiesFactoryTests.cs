using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.PageBaseModelFactories;

namespace Web.Tests.ModelFactoryTests.BaseModelFactories
{
    public class PagePropertiesFactoryTests : MockContainer
    {
        [Test]
        public void Create_WithoutHomeGame_HomegameNavModelIsNull()
        {
            var user = new User();

            var sut = GetSut();
            var result = sut.Create(user);

            Assert.IsNull(result.HomegameNavModel);
        }

        [Test]
        public void HomegameNavModel_WithOneHomeGame_IsNotNull()
        {
            var user = new User();
            var homegame = new Homegame();

            var sut = GetSut();
            var result = sut.Create(user, homegame);

            Assert.IsNotNull(result.HomegameNavModel);
        }

        private PagePropertiesFactory GetSut()
        {
            return new PagePropertiesFactory(GoogleAnalyticsModelFactoryMock.Object);
        }

    }
}
