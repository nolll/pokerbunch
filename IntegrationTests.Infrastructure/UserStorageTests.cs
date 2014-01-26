using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage;
using NUnit.Framework;

namespace IntegrationTests.Infrastructure
{
    [System.ComponentModel.Category("integration")]
    public class UserStorageTests
    {
        [Test]
        public void AddUpdateDeleteUser()
        {
            var sut = GetSut();

            var userToAdd = new RawUser
                {
                    DisplayName = "a",
                    Email = "b",
                    GlobalRole = 1,
                    Id = 0,
                    RealName = "c",
                    UserName = "d"
                };

            var addedId = sut.AddUser(userToAdd);

            var userToUpdate = new RawUser
                {
                    DisplayName = "updated",
                    Email = "b",
                    GlobalRole = 1,
                    Id = addedId,
                    RealName = "c",
                    UserName = "d"
                };

            sut.UpdateUser(userToUpdate);

            sut.DeleteUser(addedId);
        }

        private SqlServerUserStorage GetSut()
        {
            const string connectionString = "Server=AMERICA;Database=pokerbunch-test;User ID=pokerbunch;Password=3Sugfisk;Trusted_Connection=False;Encrypt=False;Connection Timeout=30;";
            var storageProvider = new SqlServerStorageProvider(connectionString);
            var rawUserFactory = new RawUserFactory();
            return new SqlServerUserStorage(storageProvider, rawUserFactory);
        }
    }
}
