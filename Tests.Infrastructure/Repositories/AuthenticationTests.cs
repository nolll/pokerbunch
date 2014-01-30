using Application.Services.Interfaces;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Services;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Infrastructure.Repositories{

	public class AuthenticationTests : MockContainer {

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
            GetMock<IWebContext>().Setup(o => o.GetCookie("token")).Returns(token);

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
            GetMock<IWebContext>().Setup(o => o.GetCookie("token")).Returns(token);

            var sut = GetSut();
			var result = sut.GetUser();

			Assert.IsNull(result);
		}

		[Test]
        public void GetUser_TokenExistsAndMatchingUserExists_ReturnsUser(){
			const string token = "a";
		    const string displayName = "b";
            GetMock<IWebContext>().Setup(o => o.GetCookie("token")).Returns(token);
			var user = new FakeUser(displayName: displayName);
            GetMock<IUserRepository>().Setup(o => o.GetByToken(token)).Returns(user);

            var sut = GetSut();
			var result = sut.GetUser();

			Assert.AreEqual(displayName, result.DisplayName);
		}

		[Test]
        public void IsLoggedIn_TokenExistsButNoMatchingUser_ReturnsFalse(){
			const string token = "a";
            GetMock<IWebContext>().Setup(o => o.GetCookie("token")).Returns(token);

            var sut = GetSut();
			var result = sut.IsLoggedIn();

			Assert.IsFalse(result);
		}

		[Test]
        public void IsLoggedIn_TokenExistsAndMatchingUserExists_ReturnsTrue(){
			const string token = "a";
		    const string displayName = "b";
            GetMock<IWebContext>().Setup(o => o.GetCookie("token")).Returns(token);
			var user = new FakeUser(displayName: displayName);
            GetMock<IUserRepository>().Setup(o => o.GetByToken(token)).Returns(user);

            var sut = GetSut();
			var result = sut.IsLoggedIn();

			Assert.IsTrue(result);
		}

		[Test]
        public void IsAdmin_WithNonAdminUser_ReturnsFalse(){
			const string token = "a";
		    const string displayName = "b";
            GetMock<IWebContext>().Setup(o => o.GetCookie("token")).Returns(token);
			var user = new FakeUser(displayName: displayName);
            GetMock<IUserRepository>().Setup(o => o.GetByToken(token)).Returns(user);

            var sut = GetSut();
            var result = sut.IsAdmin();

            Assert.IsFalse(result);
		}

		[Test]
        public void IsAdmin_WithAdminUser_ReturnsTrue(){
			const string token = "a";
		    const string displayName = "b";
            GetMock<IWebContext>().Setup(o => o.GetCookie("token")).Returns(token);
			var user = new FakeUser(displayName: displayName, globalRole: Role.Admin);
            GetMock<IUserRepository>().Setup(o => o.GetByToken(token)).Returns(user);

            var sut = GetSut();
            var result = sut.IsAdmin();

            Assert.IsTrue(result);
		}

		[Test]
        public Authentication GetSut()
        {
            return new Authentication(
                GetMock<IWebContext>().Object,
                GetMock<IUserRepository>().Object);
        }

	}
}