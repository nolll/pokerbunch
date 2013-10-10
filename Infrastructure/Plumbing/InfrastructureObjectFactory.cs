using Castle.Windsor;
using Infrastructure.Caching;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Plumbing
{
    public static class InfrastructureObjectFactory
    {
        public static void RegisterTypes(IWindsorContainer container)
        {
            // Storage
            ObjectFactory.RegisterComponent<IHomegameStorage, SqlServerHomegameStorage>(container);
            ObjectFactory.RegisterComponent<ICashgameStorage, SqlServerCashgameStorage>(container);
            ObjectFactory.RegisterComponent<ICheckpointStorage, SqlServerCheckpointStorage>(container);
            ObjectFactory.RegisterComponent<IPlayerStorage, SqlServerPlayerStorage>(container);
            ObjectFactory.RegisterComponent<IUserStorage, SqlServerUserStorage>(container);
            ObjectFactory.RegisterComponent<IStorageProvider, SqlServerStorageProvider>(container);
            ObjectFactory.RegisterComponent<ISharingStorage, SqlServerSharingStorage>(container);
            ObjectFactory.RegisterComponent<ITwitterStorage, SqlServerTwitterStorage>(container);

            // Raw Factories
            ObjectFactory.RegisterComponent<IRawHomegameFactory, RawHomegameFactory>(container);
            ObjectFactory.RegisterComponent<IRawUserFactory, RawUserFactory>(container);
            ObjectFactory.RegisterComponent<IRawCashgameFactory, RawCashgameFactory>(container);
            ObjectFactory.RegisterComponent<IRawPlayerFactory, RawPlayerFactory>(container);
            ObjectFactory.RegisterComponent<IRawCheckpointFactory, RawCheckpointFactory>(container);

            // Cache
            ObjectFactory.RegisterComponent<ICacheProvider, CacheProvider>(container);
            ObjectFactory.RegisterComponent<ICacheContainer, CacheContainer>(container);
        }
    }
}