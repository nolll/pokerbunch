using Infrastructure.Caching;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Infrastructure.Tests.Caching
{
    public class CacheBusterTests : MockContainer
    {
        [Test]
        public void UserAdded_RemovesAllUserIds()
        {
            const string key = "a";

            GetMock<ICacheKeyProvider>().Setup(o => o.UserIdsKey()).Returns(key);

            var sut = GetSut();
            sut.UserAdded();

            GetMock<ICacheContainer>().Verify(o => o.Remove(key));
        }

        [Test]
        public void UserUpdated_RemovesUserAndTokenAndEmail()
        {
            const int userId = 1;
            const string userName = "a1";
            const string email = "a2";
            const string token = "a3";
            const string userKey = "b1";
            const string tokenKey = "b2";
            const string nameKey = "b3";
            const string emailKey = "b4";
            var user = new FakeUser(id: userId, userName: userName, email: email, token: token);

            GetMock<ICacheKeyProvider>().Setup(o => o.UserKey(userId)).Returns(userKey);
            GetMock<ICacheKeyProvider>().Setup(o => o.UserIdByTokenKey(token)).Returns(tokenKey);
            GetMock<ICacheKeyProvider>().Setup(o => o.UserIdByNameOrEmailKey(userName)).Returns(nameKey);
            GetMock<ICacheKeyProvider>().Setup(o => o.UserIdByNameOrEmailKey(email)).Returns(emailKey);

            var sut = GetSut();
            sut.UserUpdated(user);

            GetMock<ICacheContainer>().Verify(o => o.Remove(userKey));
            GetMock<ICacheContainer>().Verify(o => o.Remove(tokenKey));
            GetMock<ICacheContainer>().Verify(o => o.Remove(nameKey));
            GetMock<ICacheContainer>().Verify(o => o.Remove(emailKey));
        }

        private CacheBuster GetSut()
        {
            return new CacheBuster(
                GetMock<ICacheContainer>().Object,
                GetMock<ICacheKeyProvider>().Object);
        }
    }
}
