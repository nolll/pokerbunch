using System.Collections.Generic;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.SqlServer;
using NUnit.Framework;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class SqlServerPlayerStorageTests : StorageTests
    {
        [Test]
        public void GetPlayerById_CallsStorageWithCorrectSql()
        {
            const int playerId = 1;
            const string expectedSql = "SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, p.PlayerName FROM player p WHERE p.PlayerID = 1";

            var sut = GetSut();
            sut.GetPlayerById(playerId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetPlayerList_CallsStorageWithCorrectSql()
        {
            var idList = new List<int> {1, 2, 3};
            const string expectedSql = "SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, p.PlayerName FROM player p WHERE p.PlayerID IN (1,2,3)";

            var sut = GetSut();
            sut.GetPlayerList(idList);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetPlayerIdByName_CallsStorageWithCorrectSql()
        {
            const int homegameId = 1;
            const string name = "a";
            const string expectedSql = "SELECT p.PlayerID FROM player p LEFT JOIN [user] u on p.UserID = u.UserID WHERE p.HomegameID = 1 AND (p.PlayerName = 'a' OR u.DisplayName = 'a')";

            var sut = GetSut();
            sut.GetPlayerIdByName(homegameId, name);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetPlayerIdByUserName_CallsStorageWithCorrectSql()
        {
            const int homegameId = 1;
            const string userName = "a";
            const string expectedSql = "SELECT p.PlayerID FROM player p JOIN [user] u on p.UserID = u.UserID WHERE p.HomegameID = 1 AND u.UserName = 'a'";

            var sut = GetSut();
            sut.GetPlayerIdByUserName(homegameId, userName);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetPlayerIdList_CallsStorageWithCorrectSql()
        {
            const int homegameId = 1;
            const string expectedSql = "SELECT p.PlayerID FROM player p WHERE p.HomegameID = 1";

            var sut = GetSut();
            sut.GetPlayerIdList(homegameId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void AddPlayer_CallsStorageWithCorrectSql()
        {
            const int homegameId = 1;
            const string playerName = "a";
            const string expectedSql = "INSERT INTO player (HomegameID, RoleID, Approved, PlayerName) VALUES (1, 1, 1, 'a') SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";

            var sut = GetSut();
            sut.AddPlayer(homegameId, playerName);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void AddPlayerWithUser_CallsStorageWithCorrectSql()
        {
            const int homegameId = 1;
            const int userId = 2;
            const int role = 3;
            const string expectedSql = "INSERT INTO player (HomegameID, UserID, RoleID, Approved) VALUES (1, 2, 3, 1) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";

            var sut = GetSut();
            sut.AddPlayerWithUser(homegameId, userId, role);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void JoinHomegame_CallsStorageWithCorrectSql()
        {
            const int playerId = 1;
            const int role = 2;
            const int homegameId = 3;
            const int userId = 4;
            const string expectedSql = "UPDATE player SET HomegameID = 3, PlayerName = NULL, UserID = 4, RoleID = 2, Approved = 1 WHERE PlayerID = 1";

            var sut = GetSut();
            sut.JoinHomegame(playerId, role, homegameId, userId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
            
        }

        [Test]
        public void DeletePlayer_CallsStorageWithCorrectSql()
        {
            const int playerId = 1;
            const string expectedSql = "DELETE FROM player WHERE PlayerID = 1";
            
            var sut = GetSut();
            sut.DeletePlayer(playerId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        private SqlServerPlayerStorage GetSut()
        {
            return new SqlServerPlayerStorage(
                StorageProvider,
                GetMock<IRawPlayerFactory>().Object);
        }
    }
}
