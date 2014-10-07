using System;
using System.Collections.Generic;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.SqlServer;
using NUnit.Framework;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class SqlServerCashgameStorageTests : StorageTests
    {
        [Test]
        public void AddGame_CallsStorageWithCorrectSql()
        {
            const int homegameId = 1;
            var date = DateTime.MinValue;
            const string location = "a";
            const int status = 2;
            var bunch = A.Bunch.WithId(homegameId).Build();
            var cashgame = new RawCashgame{Date = date, Location = location, Status = status};

            const string expectedSql = "INSERT INTO game (HomegameID, Location, Status, Date) VALUES (1, 'a', 2, '0001-01-01 00:00:00') SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            
            var sut = GetSut();
            sut.AddGame(bunch, cashgame);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void DeleteGame_CallsStorageWithCorrectSql()
        {
            const string expectedSql = "DELETE FROM game WHERE GameID = 1";

            var sut = GetSut();
            sut.DeleteGame(1);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetGame_CallsStorageWithCorrectSql()
        {
            const int cashgameId = 1;
            const string expectedSql = "SELECT g.GameID, g.HomegameID, g.Location, g.Status, g.Date FROM game g WHERE g.GameID = 1 ORDER BY g.GameId";

            var sut = GetSut();
            sut.GetGame(cashgameId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetRunningCashgameId_CallsStorageWithCorrectSql()
        {
            const int homegameId = 2;
            const string expectedSql = "SELECT g.GameID FROM game g WHERE g.HomegameID = 2 AND g.Status = 1";

            var sut = GetSut();
            sut.GetRunningCashgameId(homegameId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetCashgameId_CallsStorageWithCorrectSql()
        {
            const int homegameId = 1;
            const string dateStr = "a";
            const string expectedSql = "SELECT g.GameID FROM game g WHERE g.HomegameID = 1 AND g.Date = 'a'";

            var sut = GetSut();
            sut.GetCashgameId(homegameId, dateStr);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetGames_CallsStorageWithCorrectSql()
        {
            var idList = new List<int>{1, 2, 3};
            const string expectedSql = "SELECT g.GameID, g.HomegameID, g.Location, g.Status, g.Date FROM game g WHERE g.GameID IN (1,2,3) ORDER BY g.GameID";

            var sut = GetSut();
            sut.GetGames(idList);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetGameIds_NoStatusNoYear_CallsStorageWithCorrectSql()
        {
            const int homegameId = 1;
            const string expectedSql = "SELECT g.GameID FROM game g WHERE g.HomegameID = 1";

            var sut = GetSut();
            sut.GetGameIds(homegameId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetGameIds_WithStatus_CallsStorageWithCorrectSql()
        {
            const int homegameId = 1;
            const int status = 2;
            const string expectedSql = "SELECT g.GameID FROM game g WHERE g.HomegameID = 1 AND g.Status = 2";

            var sut = GetSut();
            sut.GetGameIds(homegameId, status);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetGameIds_WithYear_CallsStorageWithCorrectSql()
        {
            const int homegameId = 1;
            const int year = 2;
            const string expectedSql = "SELECT g.GameID FROM game g WHERE g.HomegameID = 1 AND YEAR(g.Date) = 2";

            var sut = GetSut();
            sut.GetGameIds(homegameId, null, year);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetGameIds_WithStatusAndYear_CallsStorageWithCorrectSql()
        {
            const int homegameId = 1;
            const int status = 2;
            const int year = 3;
            const string expectedSql = "SELECT g.GameID FROM game g WHERE g.HomegameID = 1 AND g.Status = 2 AND YEAR(g.Date) = 3";

            var sut = GetSut();
            sut.GetGameIds(homegameId, status, year);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetYears_CallsStorageWithCorrectSql()
        {
            const int homegameId = 1;
            const string expectedSql = "SELECT DISTINCT YEAR(ccp.Timestamp) as 'Year' FROM cashgamecheckpoint ccp LEFT JOIN game g ON ccp.GameID = g.GameID WHERE g.HomegameID = 1 ORDER BY 'Year' DESC";

            var sut = GetSut();
            sut.GetYears(homegameId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void UpdateGame_CallsStorageWithCorrectSql()
        {
            const string location = "a";
            var date = DateTime.MinValue;
            const int status = 1;
            const int cashgameId = 2;
            var cashgame = new RawCashgame{Location = location, Date = date, Status = status, Id = cashgameId};
            const string expectedSql = "UPDATE game SET Location = 'a', Date = '0001-01-01 00:00:00', Status = 1 WHERE GameID = 2";

            var sut = GetSut();
            sut.UpdateGame(cashgame);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void HasPlayed_CallsStorageWithCorrectSql()
        {
            const int playerId = 1;
            const string expectedSql = "SELECT DISTINCT PlayerID FROM cashgamecheckpoint WHERE PlayerId = 1";

            var sut = GetSut();
            sut.HasPlayed(playerId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetLocations_CallsStorageWithCorrectSql()
        {
            const string slug = "a";
            const string expectedSql = "SELECT DISTINCT g.Location FROM game g LEFT JOIN homegame h ON g.HomegameID = h.HomegameID WHERE Name = 'a' AND g.Location <> '' ORDER BY g.Location";

            var sut = GetSut();
            sut.GetLocations(slug);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        private SqlServerCashgameStorage GetSut()
        {
            return new SqlServerCashgameStorage(
                StorageProvider,
                GetMock<IRawCashgameFactory>().Object);
        }
    }
}
