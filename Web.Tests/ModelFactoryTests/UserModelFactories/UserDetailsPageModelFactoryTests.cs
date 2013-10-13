using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.UserModelFactories;
using Web.Models.MiscModels;
using Web.Models.UrlModels;

namespace Web.Tests.ModelFactoryTests.UserModelFactories{

	public class UserDetailsPageModelFactoryTests : MockContainer {

		[Test]
        public void ActionDetails_SetsUserData(){
			var user = new User {UserName = "a", DisplayName = "b", RealName = "c", Email = "d"};

		    var sut = GetSut();
			var result = sut.Create(user, user);

			Assert.AreEqual("a", result.UserName);
			Assert.AreEqual("b", result.DisplayName);
			Assert.AreEqual("c", result.RealName);
			Assert.AreEqual("d", result.Email);
		}

		[Test]
        public void ActionDetails_SetsAvatarModel()
		{
		    const string email = "a";
            var user = new User
			    {
			        Email = email
			    };
            WebMocks.AvatarModelFactoryMock.Setup(o => o.Create(email, It.IsAny<AvatarSize>())).Returns(new AvatarModel());

			var sut = GetSut();
			var result = sut.Create(user, user);

		    Assert.IsInstanceOf<AvatarModel>(result.AvatarModel);
        }

        [Test]
        public void ActionDetails_ViewOwnUser_OutputsEditLink(){
			var user = new User();

			var sut = GetSut();
			var result = sut.Create(user, user);

			Assert.IsTrue(result.ShowEditLink);
			Assert.IsInstanceOf<UserEditUrlModel>(result.EditLink);
		}

        [Test]
        public void ActionDetails_ViewOtherUserWithAdminUser_OutputsEditLink(){
			var currentUser = new User {UserName = "a", GlobalRole = Role.Admin};
            var displayUser = new User {UserName = "b"};

			var sut = GetSut();
			var result = sut.Create(currentUser, displayUser);

			Assert.IsTrue(result.ShowEditLink);
			Assert.IsInstanceOf<UserEditUrlModel>(result.EditLink);
		}

		[Test]
        public void ActionDetails_ViewOwnUser_OutputsChangePasswordLink(){
			var user = new User();

			var sut = GetSut();
			var result = sut.Create(user, user);

			Assert.IsTrue(result.ShowPasswordLink);
            Assert.IsInstanceOf<ChangePasswordUrlModel>(result.ChangePasswordLink);
		}

		[Test]
        public void ActionDetails_ViewOtherUser_DoesNotOutputEditLink(){
			var currentUser = new User {UserName = "a"};
		    var displayUser = new User {UserName = "b"};
		    
            var sut = GetSut();
			var result = sut.Create(currentUser, displayUser);

			Assert.IsFalse(result.ShowEditLink);
		}

        [Test]
        public void ActionDetails_ViewOtherUserWithAdminUser_DoesNotOutputPasswordLink(){
			var currentUser = new User {UserName = "a", GlobalRole = Role.Admin};
            var displayUser = new User {UserName = "b"};

            var sut = GetSut();
			var result = sut.Create(currentUser, displayUser);

			Assert.IsFalse(result.ShowPasswordLink);
		}
        
        private UserDetailsPageModelFactory GetSut()
        {
            return new UserDetailsPageModelFactory(WebMocks.AvatarModelFactoryMock.Object, WebMocks.PagePropertiesFactoryMock.Object);
        }

	}

}