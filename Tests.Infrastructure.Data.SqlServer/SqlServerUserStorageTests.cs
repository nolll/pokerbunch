using System.Collections.Generic;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.SqlServer;
using NUnit.Framework;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class SqlServerUserStorageTests : StorageTests
    {
        [Test]
        public void GetUserById_CallsStorageWithCorrectSql()
        {
            const int userId = 1;
            const string expectedSql = "SELECT u.UserID, u.UserName, u.DisplayName, u.RealName, u.Email, u.Token, u.Password, u.Salt, u.RoleID FROM [User] u WHERE u.UserId = 1";

            var sut = GetSut();
            sut.GetUserById(userId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetUserIdByNameOrEmail_CallsStorageWithCorrectSql()
        {
            const string query = "a";
            const string expectedSql = "SELECT u.UserID FROM [User] u WHERE (u.UserName = 'a' OR u.Email = 'a')";

            var sut = GetSut();
            sut.GetUserIdByNameOrEmail(query);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetUserIdByToken_CallsStorageWithCorrectSql()
        {
            const string token = "a";
            const string expectedSql = "SELECT u.UserID FROM [User] u WHERE u.Token = 'a'";

            var sut = GetSut();
            sut.GetUserIdByToken(token);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetUserList_CallsStorageWithCorrectSql()
        {
            var idList = new List<int> { 1, 2, 3 };
            const string expectedSql = "SELECT u.UserID, u.UserName, u.DisplayName, u.RealName, u.Email, u.Token, u.Password, u.Salt, u.RoleID FROM [User] u WHERE u.UserID IN(1,2,3)";

            var sut = GetSut();
            sut.GetUserList(idList);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void GetUserIdList_CallsStorageWithCorrectSql()
        {
            const string expectedSql = "SELECT u.UserID, u.UserName, u.DisplayName, u.RealName, u.Email, u.Token, u.Password, u.Salt, u.RoleID FROM [User] u ORDER BY u.DisplayName";

            var sut = GetSut();
            sut.GetUserIdList();

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void UpdateUser_CallsStorageWithCorrectSql()
        {
            const string displayName = "a";
            const string realName = "b";
            const string email = "c";
            const string token = "d";
            const string password = "e";
            const string salt = "f";
            const int userId = 1;
            var user = new RawUser{DisplayName = displayName, RealName = realName, Email = email, Token = token, EncryptedPassword = password, Salt = salt, Id = userId};
            const string expectedSql = "UPDATE [user] SET DisplayName = 'a', RealName = 'b', Email = 'c', Token = 'd', Password = 'e', Salt = 'f' WHERE UserID = 1";
            
            var sut = GetSut();
            sut.UpdateUser(user);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void AddUser_CallsStorageWithCorrectSql()
        {
            const string userName = "a";
            const string displayName = "b";
            const string email = "c";
            const string token = "d";
            const string password = "e";
            const string salt = "f";
            var user = new RawUser{UserName = userName, DisplayName = displayName, Email = email, Token = token, EncryptedPassword = password, Salt = salt};
            const string expectedSql = "INSERT INTO [user] (UserName, DisplayName, Email, RoleId, Token, Password, Salt) VALUES ('a', 'b', 'c', 1, 'd', 'e', 'f') SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";

            var sut = GetSut();
            sut.AddUser(user);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void DeleteUser_CallsStorageWithCorrectSql()
        {
            int userId = 1;
            const string expectedSql = "DELETE FROM [user] WHERE UserID = 1";

            var sut = GetSut();
            sut.DeleteUser(userId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        private SqlServerUserStorage GetSut()
        {
            return new SqlServerUserStorage(
                StorageProvider,
                GetMock<IRawUserFactory>().Object);
        }
    }
}
