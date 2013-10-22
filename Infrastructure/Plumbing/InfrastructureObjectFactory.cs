using Castle.Core;
using Castle.Windsor;
using Infrastructure.Caching;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.System;

namespace Infrastructure.Plumbing
{
    public class InfrastructureObjectFactory : ObjectFactory
    {
        public InfrastructureObjectFactory(IWindsorContainer container, LifestyleType lifestyleType = LifestyleType.PerWebRequest)
            : base(container, lifestyleType)
        {
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            // Storage
            RegisterComponent<IHomegameStorage, SqlServerHomegameStorage>();
            RegisterComponent<ICashgameStorage, SqlServerCashgameStorage>();
            RegisterComponent<ICheckpointStorage, SqlServerCheckpointStorage>();
            RegisterComponent<IPlayerStorage, SqlServerPlayerStorage>();
            RegisterComponent<IUserStorage, SqlServerUserStorage>();
            RegisterComponent<IStorageProvider, SqlServerStorageProvider>();
            RegisterComponent<ISharingStorage, SqlServerSharingStorage>();
            RegisterComponent<ITwitterStorage, SqlServerTwitterStorage>();

            // Raw Factories
            RegisterComponent<IRawHomegameFactory, RawHomegameFactory>();
            RegisterComponent<IRawUserFactory, RawUserFactory>();
            RegisterComponent<IRawCashgameFactory, RawCashgameFactory>();
            RegisterComponent<IRawPlayerFactory, RawPlayerFactory>();
            RegisterComponent<IRawCheckpointFactory, RawCheckpointFactory>();
            RegisterComponent<IRawTwitterCredentialsFactory, RawTwitterCredentialsFactory>();

            // Cache
            RegisterComponent<ICacheProvider, CacheProvider>();
            RegisterComponent<ICacheContainer, CacheContainer>();

            // System
            RegisterComponent<IGlobalization, Globalization>();
        }
    }
}