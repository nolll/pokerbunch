using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.Repositories;
using Core.Services;
using Infrastructure.Data.Storage;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using Infrastructure.System;
using Web.ModelFactories;
using Web.Validators;

namespace Web.Plumbing
{
    public static class ObjectFactory
    {
        public static void RegisterTypes(IWindsorContainer container)
        {
            // Services
            RegisterComponent<IEncryptionService, EncryptionService>(container);

            // Repositories
            RegisterComponent<IHomegameRepository, HomegameRepository>(container);
            RegisterComponent<ICashgameRepository, CashgameRepository>(container);
            RegisterComponent<IUserContext, UserContext>(container, LifestyleType.PerWebRequest);

            // Storage
            RegisterComponent<IHomegameStorage, MySqlHomegameStorage>(container);
            RegisterComponent<ICashgameStorage, MySqlCashgameStorage>(container);
            RegisterComponent<IPlayerStorage, MySqlPlayerStorage>(container);
            RegisterComponent<IUserStorage, MySqlUserStorage>(container);
            RegisterComponent<IStorageProvider, MySqlStorageProvider>(container);

            // System
            RegisterComponent<IWebContext, WebContext>(container);
            RegisterComponent<ITimeProvider, TimeProvider>(container);

            // Core Factories
            RegisterComponent<IHomegameFactory, HomegameFactory>(container);
            RegisterComponent<IUserFactory, UserFactory>(container);
            RegisterComponent<ICashgameFactory, CashgameFactory>(container);
            RegisterComponent<IPlayerFactory, PlayerFactory>(container);
            RegisterComponent<ICashgameResultFactory, CashgameResultFactory>(container);
            RegisterComponent<ICashgameTotalResultFactory, CashgameTotalResultFactory>(container);
            RegisterComponent<ICashgameSuiteFactory, CashgameSuiteFactory>(container);

            // Model Factories
            RegisterComponent<IHomeModelFactory, HomeModelFactory>(container);

            // Validator Factories
            RegisterComponent<IUserValidatorFactory, UserValidatorFactory>(container);
        }

        private static void RegisterComponent<T, TK>(IWindsorContainer container)
            where TK : class
            where T : class
        {
            container.Register(Component.For<T, TK>().LifeStyle.Is(LifestyleType.Singleton));
        }

        private static void RegisterComponent<T, TK>(IWindsorContainer container, LifestyleType lifestyleType)
            where TK : class
            where T : class
        {
            container.Register(Component.For<T, TK>().LifeStyle.Is(lifestyleType));
        }

    }
}