using Castle.Core;
using Castle.Windsor;
using Core.Repositories;
using Core.Services;
using Infrastructure.Config;
using Infrastructure.Factories;
using Infrastructure.Integration.Gravatar;
using Infrastructure.Plumbing;
using Infrastructure.Repositories;
using Infrastructure.System;
using Web.Controllers;
using Web.ModelFactories.AuthModelFactories;
using Web.ModelFactories.CashgameModelFactories.Buyin;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.HomeModelFactories;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.ModelFactories.UserModelFactories;
using Web.ModelMappers;
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
            ObjectFactory.RegisterComponent<IAvatarModelFactory, AvatarModelFactory>(container);
            ObjectFactory.RegisterComponent<IAuthLoginPageModelFactory, AuthLoginPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IAddHomegamePageModelFactory, AddHomegamePageModelFactory>(container);
            ObjectFactory.RegisterComponent<IBuyinPageModelFactory, BuyinPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IPagePropertiesFactory, PagePropertiesFactory>(container);
            ObjectFactory.RegisterComponent<IUserDetailsPageModelFactory, UserDetailsPageModelFactory>(container);

            // Validator Factories
            ObjectFactory.RegisterComponent<IUserValidatorFactory, UserValidatorFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameValidatorFactory, CashgameValidatorFactory>(container);
            ObjectFactory.RegisterComponent<IHomegameValidatorFactory, HomegameValidatorFactory>(container);

            // Mappers
            ObjectFactory.RegisterComponent<IHomegameModelMapper, HomegameModelMapper>(container);

            // Misc
            ObjectFactory.RegisterComponent<ISlugGenerator, SlugGenerator>(container);
        }

    }
}