using Infrastructure.Data.Factories;
using Infrastructure.Data.Interfaces;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Infrastructure.Data.Factories
{
    public class RawPlayerFactoryTests : TestBase
    {
        [Test]
        public void Create_AllPropertiesAreSetFromStorageDataReader()
        {
            const string playerName = "a";
            const int role = 1;
            const int userId = 2;
            const int playerId = 3;

            var readerMock = GetMock<IStorageDataReader>();
            readerMock.Setup(o => o.GetStringValue("PlayerName")).Returns(playerName);
            readerMock.Setup(o => o.GetIntValue("RoleID")).Returns(role);
            readerMock.Setup(o => o.GetIntValue("UserID")).Returns(userId);
            readerMock.Setup(o => o.GetIntValue("PlayerID")).Returns(playerId);

            var result = RawPlayerFactory.Create(readerMock.Object);

            Assert.AreEqual(playerName, result.DisplayName);
            Assert.AreEqual(role, result.Role);
            Assert.AreEqual(userId, result.UserId);
            Assert.AreEqual(playerId, result.Id);
        }
    }
}
