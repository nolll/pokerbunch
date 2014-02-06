using Application.Services;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.SqlServer;
using NUnit.Framework;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class SqlServerCashgameStorageTests : StorageTests
    {
        [Test]
        public void Method_Scenario_Expected()
        {
            var sut = GetSut();
        }

        private SqlServerCashgameStorage GetSut()
        {
            return new SqlServerCashgameStorage(
                StorageProvider,
                GetMock<IRawCashgameFactory>().Object,
                GetMock<IRawCheckpointFactory>().Object,
                GetMock<IGlobalization>().Object);
        }
    }
}
