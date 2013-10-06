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
            ObjectFactory.RegisterComponent<IHomegameStorage, MySqlHomegameStorage>(container);
            ObjectFactory.RegisterComponent<ICashgameStorage, MySqlCashgameStorage>(container);
            ObjectFactory.RegisterComponent<ICheckpointStorage, MySqlCheckpointStorage>(container);
            ObjectFactory.RegisterComponent<IPlayerStorage, MySqlPlayerStorage>(container);
            ObjectFactory.RegisterComponent<IUserStorage, MySqlUserStorage>(container);
            ObjectFactory.RegisterComponent<IStorageProvider, MySqlStorageProvider>(container);
            ObjectFactory.RegisterComponent<ISharingStorage, MySqlSharingStorage>(container);
            ObjectFactory.RegisterComponent<ITwitterStorage, MySqlTwitterStorage>(container);

            // Raw Factories
            ObjectFactory.RegisterComponent<IRawHomegameFactory, RawHomegameFactory>(container);
            ObjectFactory.RegisterComponent<IRawUserFactory, RawUserFactory>(container);
            ObjectFactory.RegisterComponent<IRawCashgameFactory, RawCashgameFactory>(container);
            ObjectFactory.RegisterComponent<IRawPlayerFactory, RawPlayerFactory>(container);

            // Cache
            ObjectFactory.RegisterComponent<ICacheProvider, CacheProvider>(container);
            ObjectFactory.RegisterComponent<ICacheContainer, CacheContainer>(container);
        }
    }
}