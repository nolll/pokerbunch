using Infrastructure.Data.SqlServer;
using NUnit.Framework;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class SqlServerSharingStorageTests : StorageTests
    {
        [Test]
        public void GetServices_CallsStorageWithCorrectSql()
        {
            const int userId = 1;
            const string expectedSql = "SELECT us.ServiceName FROM usersharing us WHERE us.UserID = 1";

            var sut = GetSut();
            sut.GetServices(userId);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void IsSharing_CallsStorageWithCorrectSql()
        {
            const int userId = 1;
            const string sharingProvider = "a";
            const string expectedSql = "SELECT us.UserID, us.ServiceName FROM usersharing us WHERE us.UserID = 1 AND us.ServiceName = 'a'";

            var sut = GetSut();
            sut.IsSharing(userId, sharingProvider);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void AddSharing_CallsStorageWithCorrectSql()
        {
            const int userId = 1;
            const string sharingProvider = "a";
            const string expectedSql = "INSERT INTO usersharing (UserID, ServiceName) OUTPUT INSERTED.UserId VALUES (1, 'a')";

            var sut = GetSut();
            sut.AddSharing(userId, sharingProvider);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        [Test]
        public void RemoveSharing_CallsStorageWithCorrectSql()
        {
            const int userId = 1;
            const string sharingProvider = "a";
            const string expectedSql = "DELETE FROM usersharing WHERE UserID = 1 AND ServiceName = 'a'";

            var sut = GetSut();
            sut.RemoveSharing(userId, sharingProvider);

            Assert.AreEqual(expectedSql, StorageProvider.Sql);
        }

        private SqlServerSharingStorage GetSut()
        {
            return new SqlServerSharingStorage(
                StorageProvider);
        }
    }
}
