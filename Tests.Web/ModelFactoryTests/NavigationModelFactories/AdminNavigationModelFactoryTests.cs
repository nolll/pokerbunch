using Application.Services;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.NavigationModelFactories;
using Web.Security;

namespace Tests.Web.ModelFactoryTests.NavigationModelFactories
{
	public class AdminNavigationModelFactoryTests : MockContainer
	{
        [Test]
		public void Show_AdminUser_DefaultContentSet()
        {
            GetMock<IAuth>().Setup(o => o.IsAdmin()).Returns(true);
            
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
		    const string homegameListUrl = "a";
            const string userListUrl = "b";

            GetMock<IUrlProvider>().Setup(o => o.GetHomegameListUrl()).Returns(homegameListUrl);
            GetMock<IUrlProvider>().Setup(o => o.GetUserListUrl()).Returns(userListUrl);
		    GetMock<IAuth>().Setup(o => o.IsAdmin()).Returns(true);

		    var sut = GetSut();
            var result = sut.Create();

            Assert.AreEqual(homegameListUrl, result.Nodes[0].UrlModel);
			Assert.AreEqual(userListUrl, result.Nodes[1].UrlModel);
		}

        private AdminNavigationModelFactory GetSut()
        {
            return new AdminNavigationModelFactory(
                GetMock<IUrlProvider>().Object,
                GetMock<IAuth>().Object);
        }

	}

}