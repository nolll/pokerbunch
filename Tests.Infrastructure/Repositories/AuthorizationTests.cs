using Application.Exceptions;
using Application.Services;
using Application.Services.Interfaces;
using Core.Classes;
using Core.Repositories;
using Infrastructure.System;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Infrastructure.Repositories
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

        [Test]
        public void RequireManager_WithManagerUser_DoesNotThrowException()
        {
            const string slug = "a";
            var homegame = new FakeHomegame();
            var user = new FakeUser();

            GetMock<IHomegameRepository>().Setup(o => o.GetByName(slug)).Returns(homegame);
            GetMock<IAuthentication>().Setup(o => o.GetUser()).Returns(user);
            GetMock<IHomegameRepository>().Setup(o => o.GetHomegameRole(homegame, user)).Returns(Role.Manager);

            var sut = GetSut();
            sut.RequireManager(slug);
        }

        [Test]
        public void RequireManager_WithPlayerUser_ThrowsException()
        {
            const string slug = "a";
            var homegame = new FakeHomegame();
            var user = new FakeUser();

            GetMock<IHomegameRepository>().Setup(o => o.GetByName(slug)).Returns(homegame);
            GetMock<IAuthentication>().Setup(o => o.GetUser()).Returns(user);
            GetMock<IHomegameRepository>().Setup(o => o.GetHomegameRole(homegame, user)).Returns(Role.Player);

            var sut = GetSut();
            
            Assert.Throws<AccessDeniedException>(() => sut.RequireManager(slug));
        }

        [Test]
        public void RequirePlayer_WithPlayerUser_DoesNotThrowException()
        {
            const string slug = "a";
            var homegame = new FakeHomegame();
            var user = new FakeUser();

            GetMock<IHomegameRepository>().Setup(o => o.GetByName(slug)).Returns(homegame);
            GetMock<IAuthentication>().Setup(o => o.GetUser()).Returns(user);
            GetMock<IHomegameRepository>().Setup(o => o.GetHomegameRole(homegame, user)).Returns(Role.Player);

            var sut = GetSut();
            sut.RequirePlayer(slug);
        }

        [Test]
        public void RequirePlayer_WithGuestUser_ThrowsException()
        {
            const string slug = "a";
            var homegame = new FakeHomegame();
            var user = new FakeUser();

            GetMock<IHomegameRepository>().Setup(o => o.GetByName(slug)).Returns(homegame);
            GetMock<IAuthentication>().Setup(o => o.GetUser()).Returns(user);
            GetMock<IHomegameRepository>().Setup(o => o.GetHomegameRole(homegame, user)).Returns(Role.Guest);

            var sut = GetSut();

            Assert.Throws<AccessDeniedException>(() => sut.RequirePlayer(slug));
        }

        [Test]
        public void CanActAsPlayer_WithAdminUser_ReturnsTrue()
        {
            const string slug = "a";
            const string playerName = "b";

            GetMock<IAuthentication>().Setup(o => o.IsAdmin()).Returns(true);

            var sut = GetSut();
            var result = sut.CanActAsPlayer(slug, playerName);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanActAsPlayer_WithSelfUser_ReturnsTrue()
        {
            const string slug = "a";
            const string playerName = "b";
            const int userId = 1;
            var homegame = new FakeHomegame(slug: slug);
            var player = new FakePlayer(userId: userId, displayName: playerName);
            var user = new FakeUser(userId);

            GetMock<IHomegameRepository>().Setup(o => o.GetByName(slug)).Returns(homegame);
            GetMock<IPlayerRepository>().Setup(o => o.GetByName(homegame, playerName)).Returns(player);
            GetMock<IAuthentication>().Setup(o => o.GetUser()).Returns(user);
            GetMock<IAuthentication>().Setup(o => o.IsAdmin()).Returns(false);

            var sut = GetSut();
            var result = sut.CanActAsPlayer(slug, playerName);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanActAsPlayer_WithOtherUser_ReturnsTrue()
        {
            const string slug = "a";
            const string playerName = "b";
            const int userId = 1;
            const int otherUserId = 2;
            var homegame = new FakeHomegame(slug: slug);
            var player = new FakePlayer(userId: otherUserId, displayName: playerName);
            var user = new FakeUser(userId);

            GetMock<IHomegameRepository>().Setup(o => o.GetByName(slug)).Returns(homegame);
            GetMock<IPlayerRepository>().Setup(o => o.GetByName(homegame, playerName)).Returns(player);
            GetMock<IAuthentication>().Setup(o => o.GetUser()).Returns(user);
            GetMock<IAuthentication>().Setup(o => o.IsAdmin()).Returns(false);

            var sut = GetSut();
            var result = sut.CanActAsPlayer(slug, playerName);

            Assert.IsFalse(result);
        }

        private Authorization GetSut()
        {
            return new Authorization(
                GetMock<IAuthentication>().Object,
                GetMock<IHomegameRepository>().Object,
                GetMock<IPlayerRepository>().Object);
        }

    }
}