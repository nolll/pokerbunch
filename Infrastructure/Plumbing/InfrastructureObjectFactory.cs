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

            // Raw Factories
            ObjectFactory.RegisterComponent<IRawHomegameFactory, RawHomegameFactory>(container);

            // Cache
            ObjectFactory.RegisterComponent<ICacheProvider, CacheProvider>(container);
            ObjectFactory.RegisterComponent<ICacheContainer, CacheContainer>(container);
        }
    }
}