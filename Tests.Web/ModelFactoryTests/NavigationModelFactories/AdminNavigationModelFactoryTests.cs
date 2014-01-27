using Application.Services.Interfaces;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.NavigationModelFactories;

namespace Tests.Web.ModelFactoryTests.NavigationModelFactories{

	public class AdminNavigationModelFactoryTests : MockContainer
	{
	    private User _user;

        [SetUp]
        public void SetUp()
        {
            _user = null;
        }

        [Test]
		public void Show_AdminUser_DefaultContentSet(){
            _user = new FakeUser(globalRole: Role.Admin);
            var sut = GetSut();
            var result = sut.Create(_user);

            Assert.AreEqual("Admin", result.Heading);
            Assert.AreEqual("admin-nav", result.CssClass);
		}

		[Test]
		public void Show_NotLoggedIn_NoNodes(){
			var sut = GetSut();
            var result = sut.Create(_user);

			Assert.AreEqual(0, result.Nodes.Count);
		}

		[Test]
		public void Show_WithNonAdminUser_NoNodes(){
            _user = new FakeUser();
			var sut = GetSut();
            var result = sut.Create(_user);

			Assert.AreEqual(0, result.Nodes.Count);
		}

		[Test]
		public void Show_WithAdminUser_SetsNodes()
		{
		    const string homegameListUrl = "a";
            const string userListUrl = "b";

            GetMock<IUrlProvider>().Setup(o => o.GetHomegameListUrl()).Returns(homegameListUrl);
            GetMock<IUrlProvider>().Setup(o => o.GetUserListUrl()).Returns(userListUrl);

            _user = new FakeUser(globalRole: Role.Admin);
		    var sut = GetSut();
            var result = sut.Create(_user);

            Assert.AreEqual(homegameListUrl, result.Nodes[0].UrlModel);
			Assert.AreEqual(userListUrl, result.Nodes[1].UrlModel);
		}

        private AdminNavigationModelFactory GetSut()
        {
            return new AdminNavigationModelFactory(
                GetMock<IUrlProvider>().Object);
        }

	}

}