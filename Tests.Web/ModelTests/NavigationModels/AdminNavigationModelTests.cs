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
            var contextResult = new AppContextResultInTest(isAdmin: true);

            var result = new AdminNavigationModel(contextResult);

            Assert.AreEqual("Admin", result.Heading);
            Assert.AreEqual("admin-nav", result.CssClass);
        }

        [Test]
        public void Show_WithNonAdminUser_NoNodes()
        {
            var contextResult = new AppContextResultInTest();

            var result = new AdminNavigationModel(contextResult);

            Assert.AreEqual(0, result.Nodes.Count);
        }

        [Test]
        public void Show_WithAdminUser_SetsNodes()
        {
            var contextResult = new AppContextResultInTest(isAdmin: true);

            var result = new AdminNavigationModel(contextResult);

            Assert.IsInstanceOf<HomegameListUrl>(result.Nodes[0].Url);
            Assert.IsInstanceOf<UserListUrl>(result.Nodes[1].Url);
        }
    }
}