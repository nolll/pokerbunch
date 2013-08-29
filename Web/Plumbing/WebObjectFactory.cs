using Castle.Core;
using Castle.Windsor;
using Core.Repositories;
using Core.Services;
using Infrastructure.Config;
using Infrastructure.Data.Storage;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;
using Infrastructure.Integration.Gravatar;
using Infrastructure.Plumbing;
using Infrastructure.Repositories;
using Infrastructure.System;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.HomeModelFactories;
using Web.Models.PlayerModels.Details;
using Web.Validators;

namespace Web.Plumbing
{
    public static class WebObjectFactory
    {
        public static void RegisterTypes(IWindsorContainer container)
        {
            InfrastructureObjectFactory.RegisterTypes(container);

            // Services
            ObjectFactory.RegisterComponent<IEncryptionService, EncryptionService>(container);
            ObjectFactory.RegisterComponent<IAvatarService, GravatarService>(container);

            // Repositories
            ObjectFactory.RegisterComponent<IHomegameRepository, HomegameRepository>(container);
            ObjectFactory.RegisterComponent<ICashgameRepository, CashgameRepository>(container);
            ObjectFactory.RegisterComponent<IPlayerRepository, PlayerRepository>(container);
            ObjectFactory.RegisterComponent<IUserContext, UserContext>(container, LifestyleType.PerWebRequest);

            // System
            ObjectFactory.RegisterComponent<IWebContext, WebContext>(container);
            ObjectFactory.RegisterComponent<ITimeProvider, TimeProvider>(container);
            ObjectFactory.RegisterComponent<ISettings, Settings>(container);

            // Core Factories
            ObjectFactory.RegisterComponent<IHomegameFactory, HomegameFactory>(container);
            ObjectFactory.RegisterComponent<IUserFactory, UserFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameFactory, CashgameFactory>(container);
            ObjectFactory.RegisterComponent<IPlayerFactory, PlayerFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameResultFactory, CashgameResultFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameTotalResultFactory, CashgameTotalResultFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameSuiteFactory, CashgameSuiteFactory>(container);

            // Model Factories
            ObjectFactory.RegisterComponent<IHomePageModelFactory, HomePageModelFactory>(container);
            ObjectFactory.RegisterComponent<IMatrixPageModelFactory, MatrixPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IAvatarModelBuilder, AvatarModelBuilder>(container);

            // Validator Factories
            ObjectFactory.RegisterComponent<IUserValidatorFactory, UserValidatorFactory>(container);
        }

    }
}