using Infrastructure.Data.SqlServer;
using NUnit.Framework;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class SqlServerHomegameStorageTests : StorageTests
    {
        [Test]
        public void Method_Scenario_Expected()
        {
            var sut = GetSut();
        }

        private SqlServerHomegameStorage GetSut()
        {
            return new SqlServerHomegameStorage(
                StorageProvider);
        }
    }
}
