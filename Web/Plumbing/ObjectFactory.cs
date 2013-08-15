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
            container.Register(Component.For<IUserStorage>().ImplementedBy<MySqlUserStorage>());
            container.Register(Component.For<IStorageProvider>().ImplementedBy<MySqlStorageProvider>());

            // System
            container.Register(Component.For<IWebContext>().ImplementedBy<WebContext>());

            // Core Factories
            container.Register(Component.For<IUserFactory>().ImplementedBy<UserFactory>());

            // Model Factories
            container.Register(Component.For<IHomeModelFactory>().ImplementedBy<HomeModelFactory>());
        }
    }
}