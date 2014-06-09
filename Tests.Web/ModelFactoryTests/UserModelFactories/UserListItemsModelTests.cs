using Application.Services;
using Core.Entities;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.ModelFactories.UserModelFactories;
using Web.Models.MiscModels;
using Web.Services;

namespace Tests.Web.ModelFactoryTests.UserModelFactories
{

    public class UserDetailsPageModelFactoryTests : MockContainer
    {

        [Test]
        public void ActionDetails_SetsUserData()
        {
            var user = new FakeUser(userName: "a", displayName: "b", realName: "c", email: "d");

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
            var user = new FakeUser(email: email);
            GetMock<IAvatarModelFactory>().Setup(o => o.Create(email, It.IsAny<AvatarSize>())).Returns(new AvatarModel());

            var sut = GetSut();
            var result = sut.Create(user, user);

            Assert.IsInstanceOf<AvatarModel>(result.AvatarModel);
        }

        [Test]
        public void ActionDetails_ViewOwnUser_OutputsEditLink()
        {
            var user = new FakeUser();

            var sut = GetSut();
            var result = sut.Create(user, user);

            Assert.IsTrue(result.ShowEditLink);
            Assert.IsInstanceOf<EditUserUrlModel>(result.EditLink);
        }

        [Test]
        public void ActionDetails_ViewOtherUserWithAdminUser_OutputsEditLink()
        {
            var currentUser = new FakeUser(userName: "a", globalRole: Role.Admin);
            var displayUser = new FakeUser(userName: "b");

            var sut = GetSut();
            var result = sut.Create(currentUser, displayUser);

            Assert.IsTrue(result.ShowEditLink);
            Assert.IsInstanceOf<EditUserUrlModel>(result.EditLink);
        }

        [Test]
        public void ActionDetails_ViewOwnUser_OutputsChangePasswordLink()
        {
            var user = new FakeUser();

            const string changePasswordUrl = "a";
            GetMock<IUrlProvider>().Setup(o => o.GetChangePasswordUrl()).Returns(changePasswordUrl);

            var sut = GetSut();
            var result = sut.Create(user, user);

            Assert.IsTrue(result.ShowPasswordLink);
            Assert.AreEqual(changePasswordUrl, result.ChangePasswordLink);
        }

        [Test]
        public void ActionDetails_ViewOtherUser_DoesNotOutputEditLink()
        {
            var currentUser = new FakeUser(userName: "a");
            var displayUser = new FakeUser(userName: "b");

            var sut = GetSut();
            var result = sut.Create(currentUser, displayUser);

            Assert.IsFalse(result.ShowEditLink);
        }

        [Test]
        public void ActionDetails_ViewOtherUserWithAdminUser_DoesNotOutputPasswordLink()
        {
            var currentUser = new FakeUser(userName: "a", globalRole: Role.Admin);
            var displayUser = new FakeUser(userName: "b");

            var sut = GetSut();
            var result = sut.Create(currentUser, displayUser);

            Assert.IsFalse(result.ShowPasswordLink);
        }

        private UserDetailsPageModelFactory GetSut()
        {
            return new UserDetailsPageModelFactory(
                GetMock<IAvatarModelFactory>().Object,
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<IUrlProvider>().Object);
        }

    }

}