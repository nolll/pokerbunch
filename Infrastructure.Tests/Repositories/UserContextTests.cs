using Core.Classes;
using Infrastructure.Repositories;
using NUnit.Framework;
using Tests.Common;

namespace CoreTests{

	public class UserContextTests : MockContainer {

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
            _webContextMock.Setup(o => o.GetCookie("token")).Returns(token);

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
			_webContextMock.Setup(o => o.GetCookie("token")).Returns(token);

            var sut = GetSut();
			var result = sut.GetUser();

			Assert.IsNull(result);
		}

		[Test]
        public void GetUser_TokenExistsAndMatchingUserExists_ReturnsUser(){
			const string token = "a";
		    const string displayName = "b";
			_webContextMock.Setup(o => o.GetCookie("token")).Returns(token);
			var user = new User{DisplayName = displayName};
		    _userStorageMock.Setup(o => o.GetUserByToken(token)).Returns(user);

            var sut = GetSut();
			var result = sut.GetUser();

			Assert.AreEqual(displayName, result.DisplayName);
		}

		[Test]
        public void GetUser_CalledTwice_TokenExists_DatabaseIsOnlyAskedOnce(){
			const string token = "a";
		    var callCount = 0;
            _webContextMock.Setup(o => o.GetCookie("token")).Returns(token);
		    _userStorageMock.Setup(o => o.GetUserByToken(token)).Callback(() => callCount++);

		    var sut = GetSut();
			sut.GetUser();
			sut.GetUser();

            Assert.AreEqual(1, callCount);
		}

		[Test]
        public void IsLoggedIn_TokenExistsButNoMatchingUser_ReturnsFalse(){
			const string token = "a";
            _webContextMock.Setup(o => o.GetCookie("token")).Returns(token);

            var sut = GetSut();
			var result = sut.IsLoggedIn();

			Assert.IsFalse(result);
		}

		[Test]
        public void IsLoggedIn_TokenExistsAndMatchingUserExists_ReturnsTrue(){
			const string token = "a";
		    const string displayName = "b";
			_webContextMock.Setup(o => o.GetCookie("token")).Returns(token);
			var user = new User{DisplayName = displayName};
		    _userStorageMock.Setup(o => o.GetUserByToken(token)).Returns(user);

            var sut = GetSut();
			var result = sut.IsLoggedIn();

			Assert.IsTrue(result);
		}

		[Test]
        public void IsAdmin_WithNonAdminUser_ReturnsFalse(){
			const string token = "a";
		    const string displayName = "b";
			_webContextMock.Setup(o => o.GetCookie("token")).Returns(token);
			var user = new User{DisplayName = displayName};
		    _userStorageMock.Setup(o => o.GetUserByToken(token)).Returns(user);

            var sut = GetSut();
            var result = sut.IsAdmin();

            Assert.IsFalse(result);
		}

		[Test]
        public void IsAdmin_WithAdminUser_ReturnsTrue(){
			const string token = "a";
		    const string displayName = "b";
			_webContextMock.Setup(o => o.GetCookie("token")).Returns(token);
			var user = new User{DisplayName = displayName, GlobalRole = Role.Admin};
		    _userStorageMock.Setup(o => o.GetUserByToken(token)).Returns(user);

            var sut = GetSut();
            var result = sut.IsAdmin();

            Assert.IsTrue(result);
		}

        [Test]
        public void IsInRole_WithManagerRoleAndPlayerUser_ReturnsFalse(){
			const string token = "a";
		    const string displayName = "b";
			_webContextMock.Setup(o => o.GetCookie("token")).Returns(token);
			var user = new User{DisplayName = displayName};
		    _userStorageMock.Setup(o => o.GetUserByToken(token)).Returns(user);
            var homegame = new Homegame();
            _homegameStorageMock.Setup(o => o.GetHomegameRole(homegame, user)).Returns((int)Role.Player);

            var sut = GetSut();
			var result = sut.IsInRole(homegame, Role.Manager);

			Assert.IsFalse(result);
		}

		[Test]
        public void IsInRole_WithPlayerRoleAndManagerUser_ReturnsTrue(){
			const string token = "a";
		    const string displayName = "b";
			_webContextMock.Setup(o => o.GetCookie("token")).Returns(token);
			var user = new User{DisplayName = displayName};
		    _userStorageMock.Setup(o => o.GetUserByToken(token)).Returns(user);
            var homegame = new Homegame();
            _homegameStorageMock.Setup(o => o.GetHomegameRole(homegame, user)).Returns((int)Role.Manager);

            var sut = GetSut();
            var result = sut.IsInRole(homegame, Role.Player);

            Assert.IsTrue(result);
		}

		[Test]
        public void IsInRole_WithAdminRoleAndAdminUser_ReturnsTrue(){
            const string token = "a";
		    const string displayName = "b";
			_webContextMock.Setup(o => o.GetCookie("token")).Returns(token);
			var user = new User{DisplayName = displayName, GlobalRole = Role.Admin};
		    _userStorageMock.Setup(o => o.GetUserByToken(token)).Returns(user);
            var homegame = new Homegame();

            var sut = GetSut();
            var result = sut.IsInRole(homegame, Role.Admin);

            Assert.IsTrue(result);
		}

        public UserContext GetSut()
        {
            return new UserContext(_webContextMock.Object, _userStorageMock.Object, _homegameStorageMock.Object);
        }

	}

}