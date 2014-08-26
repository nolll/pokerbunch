using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.SqlServer;
using NUnit.Framework;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class SqlServerTwitterStorageTests : StorageTests
    {
        [Test]
        public void GetCredentials_CallsStorageWithCorrectSql()
        {
            const int userId = 1;
            const string expectedSql = "SELECT ut.UserId, ut.TwitterName, ut.[Key], ut.Secret FROM usertwitter ut WHERE ut.UserId = 1";

            var sut = GetSut();
            sut.GetCredentials(userId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void AddCredentials_CallsStorageWithCorrectSql()
        {
            const int userId = 1;
            const string twitterName = "a";
            const string key = "b";
            const string secret = "c";
            var credentials = new RawTwitterCredentials{Key = key, Secret = secret, TwitterName = twitterName};
            const string expectedSql = "INSERT INTO usertwitter (UserId, TwitterName, [Key], Secret) VALUES (1, 'a', 'b', 'c')";

            var sut = GetSut();
            sut.AddCredentials(userId, credentials);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void ClearCredentials_CallsStorageWithCorrectSql()
        {
            const int userId = 1;
            const string expectedSql = "DELETE FROM usertwitter WHERE UserId = 1";

            var sut = GetSut();
            sut.ClearCredentials(userId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        private SqlServerTwitterStorage GetSut()
        {
            return new SqlServerTwitterStorage(
                StorageProvider);
        }
    }
}
