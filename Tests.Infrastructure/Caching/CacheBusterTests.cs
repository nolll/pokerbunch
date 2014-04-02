using Infrastructure.Data.Cache;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Infrastructure.Caching
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
        public void UserUpdated_RemovesUserAndEmail()
        {
            const int userId = 1;
            const string userName = "a1";
            const string email = "a2";
            const string userKey = "b1";
            const string nameKey = "b3";
            const string emailKey = "b4";
            var user = new FakeUser(userId, userName, email: email);

            GetMock<ICacheKeyProvider>().Setup(o => o.UserKey(userId)).Returns(userKey);
            GetMock<ICacheKeyProvider>().Setup(o => o.UserIdByNameOrEmailKey(userName)).Returns(nameKey);
            GetMock<ICacheKeyProvider>().Setup(o => o.UserIdByNameOrEmailKey(email)).Returns(emailKey);

            var sut = GetSut();
            sut.UserUpdated(user);

            GetMock<ICacheContainer>().Verify(o => o.Remove(userKey));
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
