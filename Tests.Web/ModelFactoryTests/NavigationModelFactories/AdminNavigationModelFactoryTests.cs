using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.NavigationModelFactories;
using Web.Security;
using Web.Services;

namespace Tests.Web.ModelFactoryTests.NavigationModelFactories
{
    public class AdminNavigationModelFactoryTests : MockContainer
    {
        [Test]
        public void Show_AdminUser_DefaultContentSet()
        {
            GetMock<IAuth>().Setup(o => o.IsAdmin).Returns(true);

            var sut = GetSut();
            var result = sut.Create();

            Assert.AreEqual("Admin", result.Heading);
            Assert.AreEqual("admin-nav", result.CssClass);
        }

        [Test]
        public void Show_WithNonAdminUser_NoNodes()
        {
            var sut = GetSut();
            var result = sut.Create();

            Assert.AreEqual(0, result.Nodes.Count);
        }

        [Test]
        public void Show_WithAdminUser_SetsNodes()
        {
            GetMock<IAuth>().Setup(o => o.IsAdmin).Returns(true);

            var sut = GetSut();
            var result = sut.Create();

            Assert.IsInstanceOf<HomegameListUrlModel>(result.Nodes[0].UrlModel);
            Assert.IsInstanceOf<UserListUrlModel>(result.Nodes[1].UrlModel);
        }

        private AdminNavigationModelFactory GetSut()
        {
            return new AdminNavigationModelFactory(
                GetMock<IAuth>().Object);
        }
    }
}