using Core.Classes;
using Infrastructure.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Infrastructure.Tests.Repositories{

	public class UserContextTests : InfrastructureMockContainer {

		[Test]
        public void GetToken_NoTokenExists_ReturnsNull()
		{
		    var sut = GetSut();
            var result = sut.GetToken();

			Assert.IsNull(result);
		}

        [Test]
        public void GetToken_TokenExists_ReturnsToken()
        {
            const string token = "a";
            Mocks.WebContextMock.Setup(o => o.GetCookie("token")).Returns(token);

            var sut = GetSut();
			var result = sut.GetToken();

			Assert.AreEqual(token, result);
		}

        [Test]
        public void GetUser_NoTokenExists_ReturnsNullUser(){
		    var sut = GetSut();
            var result = sut.GetUser();

			Assert.IsNull(result);
		}

		[Test]
        public void GetUser_TokenExistsButNoMatchingUser_ReturnsNull()
		{
		    const string token = "a";
            Mocks.WebContextMock.Setup(o => o.GetCookie("token")).Returns(token);

            var sut = GetSut();
			var result = sut.GetUser();

			Assert.IsNull(result);
		}

		[Test]
        public void GetUser_TokenExistsAndMatchingUserExists_ReturnsUser(){
			const string token = "a";
		    const string displayName = "b";
            Mocks.WebContextMock.Setup(o => o.GetCookie("token")).Returns(token);
			var user = new FakeUser(displayName: displayName);
            Mocks.UserRepositoryMock.Setup(o => o.GetUserByToken(token)).Returns(user);

            var sut = GetSut();
			var result = sut.GetUser();

			Assert.AreEqual(displayName, result.DisplayName);
		}

		[Test]
        public void IsLoggedIn_TokenExistsButNoMatchingUser_ReturnsFalse(){
			const string token = "a";
            Mocks.WebContextMock.Setup(o => o.GetCookie("token")).Returns(token);

            var sut = GetSut();
			var result = sut.IsLoggedIn();

			Assert.IsFalse(result);
		}

		[Test]
        public void IsLoggedIn_TokenExistsAndMatchingUserExists_ReturnsTrue(){
			const string token = "a";
		    const string displayName = "b";
            Mocks.WebContextMock.Setup(o => o.GetCookie("token")).Returns(token);
			var user = new FakeUser(displayName: displayName);
            Mocks.UserRepositoryMock.Setup(o => o.GetUserByToken(token)).Returns(user);

            var sut = GetSut();
			var result = sut.IsLoggedIn();

			Assert.IsTrue(result);
		}

		[Test]
        public void IsAdmin_WithNonAdminUser_ReturnsFalse(){
			const string token = "a";
		    const string displayName = "b";
            Mocks.WebContextMock.Setup(o => o.GetCookie("token")).Returns(token);
			var user = new FakeUser(displayName: displayName);
            Mocks.UserRepositoryMock.Setup(o => o.GetUserByToken(token)).Returns(user);

            var sut = GetSut();
            var result = sut.IsAdmin();

            Assert.IsFalse(result);
		}

		[Test]
        public void IsAdmin_WithAdminUser_ReturnsTrue(){
			const string token = "a";
		    const string displayName = "b";
            Mocks.WebContextMock.Setup(o => o.GetCookie("token")).Returns(token);
			var user = new FakeUser(displayName: displayName, globalRole: Role.Admin);
            Mocks.UserRepositoryMock.Setup(o => o.GetUserByToken(token)).Returns(user);

            var sut = GetSut();
            var result = sut.IsAdmin();

            Assert.IsTrue(result);
		}

        [Test]
        public void IsInRole_WithManagerRoleAndPlayerUser_ReturnsFalse(){
			const string token = "a";
		    const string displayName = "b";
            Mocks.WebContextMock.Setup(o => o.GetCookie("token")).Returns(token);
			var user = new FakeUser(displayName: displayName);
            Mocks.UserRepositoryMock.Setup(o => o.GetUserByToken(token)).Returns(user);
            var homegame = new FakeHomegame();
            Mocks.HomegameRepositoryMock.Setup(o => o.GetHomegameRole(homegame, user)).Returns(Role.Player);

            var sut = GetSut();
			var result = sut.IsInRole(homegame, Role.Manager);

			Assert.IsFalse(result);
		}

		[Test]
        public void IsInRole_WithPlayerRoleAndManagerUser_ReturnsTrue(){
			const string token = "a";
		    const string displayName = "b";
            Mocks.WebContextMock.Setup(o => o.GetCookie("token")).Returns(token);
			var user = new FakeUser(displayName: displayName);
            Mocks.UserRepositoryMock.Setup(o => o.GetUserByToken(token)).Returns(user);
            var homegame = new FakeHomegame();
            Mocks.HomegameRepositoryMock.Setup(o => o.GetHomegameRole(homegame, user)).Returns(Role.Manager);

            var sut = GetSut();
            var result = sut.IsInRole(homegame, Role.Player);

            Assert.IsTrue(result);
		}

		[Test]
        public void IsInRole_WithAdminRoleAndAdminUser_ReturnsTrue(){
            const string token = "a";
		    const string displayName = "b";
            Mocks.WebContextMock.Setup(o => o.GetCookie("token")).Returns(token);
			var user = new FakeUser(displayName: displayName, globalRole: Role.Admin);
            Mocks.UserRepositoryMock.Setup(o => o.GetUserByToken(token)).Returns(user);
            var homegame = new FakeHomegame();

            var sut = GetSut();
            var result = sut.IsInRole(homegame, Role.Admin);

            Assert.IsTrue(result);
		}

        public UserContext GetSut()
        {
            return new UserContext(
                Mocks.WebContextMock.Object,
                Mocks.UserRepositoryMock.Object,
                Mocks.HomegameRepositoryMock.Object);
        }

	}

}