using Application.Urls;
using NUnit.Framework;
using Tests.Common.FakeClasses;
using Web.Models.NavigationModels;

namespace Tests.Web.ModelTests.NavigationModels
{
    public class AdminNavigationModelTests
    {
        [Test]
        public void Show_AdminUser_DefaultContentSet()
        {
            var appContextResult = new AppContextResultInTest(isAdmin: true);

            var result = new AdminNavigationModel(appContextResult);

            Assert.AreEqual("Admin", result.Heading);
            Assert.AreEqual("admin-nav", result.CssClass);
        }

        [Test]
        public void Show_WithNonAdminUser_NoNodes()
        {
            var appContextResult = new AppContextResultInTest();

            var result = new AdminNavigationModel(appContextResult);

            Assert.AreEqual(0, result.Nodes.Count);
        }

        [Test]
        public void Show_WithAdminUser_SetsNodes()
        {
            var appContextResult = new AppContextResultInTest(isAdmin: true);

            var result = new AdminNavigationModel(appContextResult);

            Assert.IsInstanceOf<HomegameListUrl>(result.Nodes[0].Url);
            Assert.IsInstanceOf<UserListUrl>(result.Nodes[1].Url);
        }
    }
}