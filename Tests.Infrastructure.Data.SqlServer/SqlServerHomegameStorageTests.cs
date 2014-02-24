using System.Collections.Generic;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.SqlServer;
using NUnit.Framework;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class SqlServerHomegameStorageTests : StorageTests
    {
        [Test]
        public void GetAllIds_CallsStorageWithCorrectSql()
        {
            const string expectedSql = "SELECT h.HomegameID FROM homegame h";

            var sut = GetSut();
            sut.GetAllIds();

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetIdBySlug_CallsStorageWithCorrectSql()
        {
            const string expectedSql = "SELECT h.HomegameID FROM homegame h WHERE h.Name = 'a'";
            const string slug = "a";

            var sut = GetSut();
            sut.GetIdBySlug(slug);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetHomegames_CallsStorageWithCorrectSql()
        {
            var idList = new List<int> { 1, 2, 3 };
            const string expectedSql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h WHERE h.HomegameID IN(1,2,3)";

            var sut = GetSut();
            sut.GetHomegames(idList);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetHomegamesByUserId_CallsStorageWithCorrectSql()
        {
            const int userId = 1;
            const string expectedSql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h INNER JOIN player p on h.HomegameID = p.HomegameID WHERE p.UserID = 1 ORDER BY h.Name";

            var sut = GetSut();
            sut.GetHomegamesByUserId(userId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetHomegameByName_CallsStorageWithCorrectSql()
        {
            const string slug = "a";
            const string expectedSql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h WHERE Name = 'a'";

            var sut = GetSut();
            sut.GetHomegameByName(slug);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetById_CallsStorageWithCorrectSql()
        {
            const int id = 1;
            const string expectedSql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h WHERE HomegameID = 1";

            var sut = GetSut();
            sut.GetById(id);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetHomegameRole_CallsStorageWithCorrectSql()
        {
            const int homegameId = 1;
            const int userId = 2;
            const string expectedSql = "SELECT p.RoleID FROM player p WHERE p.UserID = 2 AND p.HomegameID = 1";

            var sut = GetSut();
            sut.GetHomegameRole(homegameId, userId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void AddHomegame_CallsStorageWithCorrectSql()
        {
            var rawHomegame = new RawHomegame
            {
                Slug = "a",
                DisplayName = "b",
                Description = "c",
                CurrencySymbol = "d",
                CurrencyLayout = "e",
                TimezoneName = "f",
                CashgamesEnabled = true,
                TournamentsEnabled = true,
                VideosEnabled = true,
                HouseRules = "g"
            };
            const string expectedSql = "INSERT INTO homegame (Name, DisplayName, Description, Currency, CurrencyLayout, Timezone, DefaultBuyin, CashgamesEnabled, TournamentsEnabled, VideosEnabled, HouseRules) VALUES ('a', 'b', 'c', 'd', 'e', 'f', 0, 1, 1, 1, 'g') SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";

            var sut = GetSut();
            sut.AddHomegame(rawHomegame);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void UpdateHomegame_CallsStorageWithCorrectSql()
        {
            var rawHomegame = new RawHomegame
            {
                Slug = "a",
                DisplayName = "b",
                Description = "c",
                HouseRules = "d",
                CurrencySymbol = "e",
                CurrencyLayout = "f",
                TimezoneName = "g",
                DefaultBuyin = 1,
                CashgamesEnabled = true,
                TournamentsEnabled = true,
                VideosEnabled = true,
                Id = 2
            };
            const string expectedSql = "UPDATE homegame SET Name = 'a', DisplayName = 'b', Description = 'c', HouseRules = 'd', Currency = 'e', CurrencyLayout = 'f', Timezone = 'g', DefaultBuyin = 1, CashgamesEnabled = 1, TournamentsEnabled = 1, VideosEnabled = 1 WHERE HomegameID = 2";

            var sut = GetSut();
            sut.UpdateHomegame(rawHomegame);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void DeleteHomegame_CallsStorageWithCorrectSql()
        {
            const string slug = "a";
            const string expectedSql = "DELETE FROM homegame WHERE Name = 'a'";

            var sut = GetSut();
            sut.DeleteHomegame(slug);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        private SqlServerHomegameStorage GetSut()
        {
            return new SqlServerHomegameStorage(
                StorageProvider,
                GetMock<IRawHomegameFactory>().Object);
        }
    }
}
