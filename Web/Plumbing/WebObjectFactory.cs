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
using Web.ModelFactories.AuthModelFactories;
using Web.ModelFactories.CashgameModelFactories;
using Web.ModelFactories.CashgameModelFactories.Buyin;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.HomeModelFactories;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.ModelFactories.PlayerModelFactories;
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

            // Page Model Factories
            ObjectFactory.RegisterComponent<IHomePageModelFactory, HomePageModelFactory>(container);
            ObjectFactory.RegisterComponent<IMatrixPageModelFactory, MatrixPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IAuthLoginPageModelFactory, AuthLoginPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IAddHomegamePageModelFactory, AddHomegamePageModelFactory>(container);
            ObjectFactory.RegisterComponent<IAddHomegameConfirmationPageModelFactory, AddHomegameConfirmationPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IBuyinPageModelFactory, BuyinPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IUserDetailsPageModelFactory, UserDetailsPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IPlayerListingPageModelFactory, PlayerListingPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IPlayerDetailsPageModelFactory, PlayerDetailsPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IHomegameListingPageModelFactory, HomegameListingPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IHomegameDetailsPageModelFactory, HomegameDetailsPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IActionPageModelFactory, ActionPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IAddCashgamePageModelFactory, AddCashgamePageModelFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameChartPageModelFactory, CashgameChartPageModelFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameDetailsPageModelFactory, CashgameDetailsPageModelFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameFactsPageModelFactory, CashgameFactsPageModelFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameLeaderboardPageModelFactory, CashgameLeaderboardPageModelFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameListingPageModelFactory, CashgameListingPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IRunningCashgamePageModelFactory, RunningCashgamePageModelFactory>(container);

            // Model Factories
            ObjectFactory.RegisterComponent<IAvatarModelFactory, AvatarModelFactory>(container);
            ObjectFactory.RegisterComponent<IPagePropertiesFactory, PagePropertiesFactory>(container);
            
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