using Application.UseCases.ApplicationContext;
using Application.UseCases.CashgameContext;
using NUnit.Framework;
using Tests.Common.FakeClasses;
using Web.Models.NavigationModels;
using Web.Models.UrlModels;

namespace Tests.Web.ModelTests.NavigationModels
{
    public class AdminNavigationModelTests
    {
        [Test]
        public void Show_AdminUser_DefaultContentSet()
        {
            var applicationContextResult = new ApplicationContextResultInTest(isAdmin: true);

            var result = new AdminNavigationModel(applicationContextResult);

            Assert.AreEqual("Admin", result.Heading);
            Assert.AreEqual("admin-nav", result.CssClass);
        }

        [Test]
        public void Show_WithNonAdminUser_NoNodes()
        {
            var applicationContextResult = new ApplicationContextResultInTest();

            var result = new AdminNavigationModel(applicationContextResult);

            Assert.AreEqual(0, result.Nodes.Count);
        }

        [Test]
        public void Show_WithAdminUser_SetsNodes()
        {
            var applicationContextResult = new ApplicationContextResultInTest(isAdmin: true);

            var result = new AdminNavigationModel(applicationContextResult);

            Assert.IsInstanceOf<HomegameListUrlModel>(result.Nodes[0].Url);
            Assert.IsInstanceOf<UserListUrlModel>(result.Nodes[1].Url);
        }
    }
}