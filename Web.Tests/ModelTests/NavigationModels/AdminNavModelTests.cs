using Core.Classes;
using NUnit.Framework;
using Web.Models.NavigationModels;
using Web.Models.UrlModels;

namespace Web.Tests.ModelTests.NavigationModels{

	public class AdminNavModelTests
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

			Assert.AreEqual("Admin", sut.Heading);
			Assert.AreEqual("admin-nav", sut.CssClass);
		}

		[Test]
		public void Show_NotLoggedIn_NoNodes(){
			var sut = GetSut();

			Assert.AreEqual(0, sut.Nodes.Count);
		}

		[Test]
		public void Show_WithNonAdminUser_NoNodes(){
			_user = new User();
			var sut = GetSut();

			Assert.AreEqual(0, sut.Nodes.Count);
		}

		[Test]
		public void Show_WithAdminUser_SetsNodes(){
			_user = new User {GlobalRole = Role.Admin};
		    var sut = GetSut();

			Assert.IsInstanceOf<HomegameListingUrlModel>(sut.Nodes[0].UrlModel);
			Assert.IsInstanceOf<UserListingUrlModel>(sut.Nodes[1].UrlModel);
		}

        private AdminNavModel GetSut()
        {
            return new AdminNavModel(_user);
        }

	}

}