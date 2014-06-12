using NUnit.Framework;
using Tests.Common;

namespace Tests.Infrastructure.Data.SqlServer
{
    public abstract class StorageTests : MockContainer
    {
        protected StorageProviderInTest StorageProvider;

        [SetUp]
        public void SetUpStorageTests()
        {
            StorageProvider = new StorageProviderInTest();
        }
    }
}