using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.Repositories;
using Infrastructure.Data.Storage;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using Infrastructure.System;
using Web.ModelFactories;

namespace Web.Plumbing
{
    public static class ObjectFactory
    {
        public static void RegisterTypes(IWindsorContainer container)
        {
            // Repositories
            container.Register(Component.For<IHomegameRepository>().ImplementedBy<HomegameRepository>());
            container.Register(Component.For<ICashgameRepository>().ImplementedBy<CashgameRepository>());
            container.Register(Component.For<IUserContext>().ImplementedBy<UserContext>()); // Maybe per web request

            // Storage
            container.Register(Component.For<IHomegameStorage>().ImplementedBy<MySqlHomegameStorage>());
            container.Register(Component.For<ICashgameStorage>().ImplementedBy<MySqlCashgameStorage>());
            container.Register(Component.For<IPlayerStorage>().ImplementedBy<MySqlPlayerStorage>());
            container.Register(Component.For<IUserStorage>().ImplementedBy<MySqlUserStorage>());
            container.Register(Component.For<IStorageProvider>().ImplementedBy<MySqlStorageProvider>());

            // System
            container.Register(Component.For<IWebContext>().ImplementedBy<WebContext>());
            container.Register(Component.For<ITimeProvider>().ImplementedBy<TimeProvider>());

            // Core Factories
            container.Register(Component.For<IUserFactory>().ImplementedBy<UserFactory>());
            container.Register(Component.For<ICashgameFactory>().ImplementedBy<CashgameFactory>());
            container.Register(Component.For<ICashgameResultFactory>().ImplementedBy<CashgameResultFactory>());
            container.Register(Component.For<ICashgameTotalResultFactory>().ImplementedBy<CashgameTotalResultFactory>());
            container.Register(Component.For<ICashgameSuiteFactory>().ImplementedBy<CashgameSuiteFactory>());

            // Model Factories
            container.Register(Component.For<IHomeModelFactory>().ImplementedBy<HomeModelFactory>());
        }
    }
}