using Castle.Core;
using Castle.Windsor;
using Core.Repositories;
using Core.Services;
using Core.Services.Interfaces;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Mappers;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.SqlServer;
using Infrastructure.System;
using Infrastructure.Web;

namespace Plumbing
{
    public class ApplicationDependencyResolver : DependencyResolver
    {
        protected ApplicationDependencyResolver(IWindsorContainer container, LifestyleType lifestyleType = LifestyleType.PerWebRequest)
            : base(container, lifestyleType)
        {
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            // Storage
            RegisterComponent<IBunchStorage, SqlServerBunchStorage>();
            RegisterComponent<ICashgameStorage, SqlServerCashgameStorage>();
            RegisterComponent<ICheckpointStorage, SqlServerCheckpointStorage>();
            RegisterComponent<IPlayerStorage, SqlServerPlayerStorage>();
            RegisterComponent<IUserStorage, SqlServerUserStorage>();
            RegisterComponent<IStorageProvider, SqlServerStorageProvider>();

            // Raw Factories
            RegisterComponent<IRawCashgameFactory, RawCashgameFactory>();

            // Cache
            RegisterComponent<ICacheProvider, CacheProvider>();
            RegisterComponent<ICacheContainer, CacheContainer>();
            RegisterComponent<ICacheBuster, CacheBuster>();

            // System
            RegisterComponent<IWebContext, WebContext>();
            RegisterComponent<ITimeProvider, TimeProvider>();

            // Repositories
            RegisterComponent<IBunchRepository, BunchRepository>();
            RegisterComponent<ICashgameRepository, CashgameRepository>();
            RegisterComponent<IPlayerRepository, PlayerRepository>();
            RegisterComponent<IUserRepository, UserRepository>();
            RegisterComponent<ICheckpointRepository, CheckpointRepository>();

            // Data Mappers
            RegisterComponent<IPlayerDataMapper, PlayerDataMapper>();

            // Services
            RegisterComponent<IAuth, Auth>();
            RegisterComponent<IMessageSender, MessageSender>();
            RegisterComponent<ICashgameService, CashgameService>();
            RegisterComponent<IRandomService, RandomService>();
        }
    }
}