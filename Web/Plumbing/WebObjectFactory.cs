using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.Repositories;
using Core.Services;
using Infrastructure.Data.Storage;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;
using Infrastructure.Plumbing;
using Infrastructure.Repositories;
using Infrastructure.System;
using Web.ModelFactories;
using Web.Validators;
using app;

namespace Web.Plumbing
{
    public static class WebObjectFactory
    {
        public static void RegisterTypes(IWindsorContainer container)
        {
            // Services
            ObjectFactory.RegisterComponent<IEncryptionService, EncryptionService>(container);

            // Repositories
            ObjectFactory.RegisterComponent<IHomegameRepository, HomegameRepository>(container);
            ObjectFactory.RegisterComponent<ICashgameRepository, CashgameRepository>(container);
            ObjectFactory.RegisterComponent<IUserContext, UserContext>(container, LifestyleType.PerWebRequest);

            // Storage
            ObjectFactory.RegisterComponent<IHomegameStorage, MySqlHomegameStorage>(container);
            ObjectFactory.RegisterComponent<ICashgameStorage, MySqlCashgameStorage>(container);
            ObjectFactory.RegisterComponent<ICheckpointStorage, MySqlCheckpointStorage>(container);
            ObjectFactory.RegisterComponent<IPlayerStorage, MySqlPlayerStorage>(container);
            ObjectFactory.RegisterComponent<IUserStorage, MySqlUserStorage>(container);
            ObjectFactory.RegisterComponent<IStorageProvider, MySqlStorageProvider>(container);

            // System
            ObjectFactory.RegisterComponent<IWebContext, WebContext>(container);
            ObjectFactory.RegisterComponent<ITimeProvider, TimeProvider>(container);

            // Core Factories
            ObjectFactory.RegisterComponent<IHomegameFactory, HomegameFactory>(container);
            ObjectFactory.RegisterComponent<IUserFactory, UserFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameFactory, CashgameFactory>(container);
            ObjectFactory.RegisterComponent<IPlayerFactory, PlayerFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameResultFactory, CashgameResultFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameTotalResultFactory, CashgameTotalResultFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameSuiteFactory, CashgameSuiteFactory>(container);

            // Model Factories
            ObjectFactory.RegisterComponent<IHomeModelFactory, HomeModelFactory>(container);
            ObjectFactory.RegisterComponent<IMatrixPageModelFactory, MatrixPageModelFactory>(container);

            // Validator Factories
            ObjectFactory.RegisterComponent<IUserValidatorFactory, UserValidatorFactory>(container);
        }

    }
}