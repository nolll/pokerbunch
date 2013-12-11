using Core.Classes;
using Core.Repositories;
using Core.Services;
using Infrastructure.System;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Infrastructure.Tests.Repositories
{
    public class AuthorizationTests : MockContainer
    {
        [Test]
        public void IsInRole_WithManagerRoleAndPlayerUser_ReturnsFalse()
        {
            const string token = "a";
            const string displayName = "b";
            GetMock<IWebContext>().Setup(o => o.GetCookie("token")).Returns(token);
            var user = new FakeUser(displayName: displayName);
            GetMock<IUserRepository>().Setup(o => o.GetByToken(token)).Returns(user);
            var homegame = new FakeHomegame();
            GetMock<IHomegameRepository>().Setup(o => o.GetHomegameRole(homegame, user)).Returns(Role.Player);

            var sut = GetSut();
            var result = sut.IsInRole(homegame, Role.Manager);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsInRole_WithPlayerRoleAndManagerUser_ReturnsTrue()
        {
            const string displayName = "b";
            var user = new FakeUser(displayName: displayName);
            GetMock<IAuthentication>().Setup(o => o.GetUser()).Returns(user);
            var homegame = new FakeHomegame();
            GetMock<IHomegameRepository>().Setup(o => o.GetHomegameRole(homegame, user)).Returns(Role.Manager);

            var sut = GetSut();
            var result = sut.IsInRole(homegame, Role.Player);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsInRole_WithAdminRoleAndAdminUser_ReturnsTrue()
        {
            GetMock<IAuthentication>().Setup(o => o.IsAdmin()).Returns(true);
            var homegame = new FakeHomegame();

            var sut = GetSut();
            var result = sut.IsInRole(homegame, Role.Admin);

            Assert.IsTrue(result);
        }

        public Authorization GetSut()
        {
            return new Authorization(
                GetMock<IAuthentication>().Object,
                GetMock<IHomegameRepository>().Object);
        }

    }
}