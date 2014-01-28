using Application.Services;
using Application.Services.Interfaces;
using Castle.Core;
using Castle.Windsor;
using Core.Repositories;
using Infrastructure.Caching;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.System;

namespace Plumbing
{
    public class InfrastructureDependencyResolver : DependencyResolver
    {
        public InfrastructureDependencyResolver(IWindsorContainer container, LifestyleType lifestyleType = LifestyleType.PerWebRequest)
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
            RegisterComponent<ICacheBuster, CacheBuster>();
            RegisterComponent<ICacheKeyProvider, CacheKeyProvider>();

            // System
            RegisterComponent<IGlobalization, Globalization>();

            // Repositories
            RegisterComponent<IHomegameRepository, HomegameRepository>();
            RegisterComponent<ICashgameRepository, CashgameRepository>();
            RegisterComponent<IPlayerRepository, PlayerRepository>();
            RegisterComponent<IUserRepository, UserRepository>();
            RegisterComponent<ITwitterRepository, TwitterRepository>();
            RegisterComponent<ISharingRepository, SharingRepository>();
            RegisterComponent<ICheckpointRepository, CheckpointRepository>();
            RegisterComponent<IAuthentication, Authentication>();
            RegisterComponent<IAuthorization, Authorization>();
        }
    }
}