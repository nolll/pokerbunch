using Core.Classes;
using NUnit.Framework;
using Web.ModelFactories.NavigationModelFactories;
using Web.Models.UrlModels;

namespace Web.Tests.ModelFactoryTests.NavigationModelFactories{

	public class AdminNavigationModelFactoryTests
	{
	    private User _user;

        [SetUp]
        public void SetUp()
        {
            _user = null;
        }

        [Test]
		public void Show_AdminUser_DefaultContentSet(){
			_user = new User {GlobalRole = Role.Admin};
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
			_user = new User();
			var sut = GetSut();
            var result = sut.Create(_user);

			Assert.AreEqual(0, result.Nodes.Count);
		}

		[Test]
		public void Show_WithAdminUser_SetsNodes(){
			_user = new User {GlobalRole = Role.Admin};
		    var sut = GetSut();
            var result = sut.Create(_user);

			Assert.IsInstanceOf<HomegameListingUrlModel>(result.Nodes[0].UrlModel);
			Assert.IsInstanceOf<UserListingUrlModel>(result.Nodes[1].UrlModel);
		}

        private AdminNavigationModelFactory GetSut()
        {
            return new AdminNavigationModelFactory();
        }

	}

}