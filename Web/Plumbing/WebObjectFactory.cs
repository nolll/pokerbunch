﻿using Castle.Core;
using Castle.Windsor;
using Core.Repositories;
using Core.Services;
using Infrastructure.Config;
using Infrastructure.Factories;
using Infrastructure.Integration.Gravatar;
using Infrastructure.Plumbing;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.System;
using Web.ModelFactories.AuthModelFactories;
using Web.ModelFactories.CashgameModelFactories;
using Web.ModelFactories.HomeModelFactories;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.ModelFactories.PlayerModelFactories;
using Web.ModelFactories.UserModelFactories;
using Web.ModelMappers;
using Web.Services;
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
            ObjectFactory.RegisterComponent<IInvitationCodeCreator, InvitationCodeCreator>(container);
            ObjectFactory.RegisterComponent<IInvitationSender, InvitationSender>(container);
            ObjectFactory.RegisterComponent<IMessageSender, MessageSender>(container);
            ObjectFactory.RegisterComponent<IUrlProvider, UrlProvider>(container);
            ObjectFactory.RegisterComponent<IUserService, UserService>(container);
            ObjectFactory.RegisterComponent<IPasswordGenerator, PasswordGenerator>(container);
            ObjectFactory.RegisterComponent<ISaltGenerator, SaltGenerator>(container);
            ObjectFactory.RegisterComponent<IRegistrationConfirmationSender, RegistrationConfirmationSender>(container);
            ObjectFactory.RegisterComponent<ISlugGenerator, SlugGenerator>(container);

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
            ObjectFactory.RegisterComponent<IReportPageModelFactory, ReportPageModelFactory>(container);
            ObjectFactory.RegisterComponent<ICashoutPageModelFactory, CashoutPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IEndPageModelFactory, EndPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IUserDetailsPageModelFactory, UserDetailsPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IPlayerListingPageModelFactory, PlayerListingPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IPlayerDetailsPageModelFactory, PlayerDetailsPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IHomegameListingPageModelFactory, HomegameListingPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IHomegameDetailsPageModelFactory, HomegameDetailsPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IHomegameEditPageModelFactory, HomegameEditPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IActionPageModelFactory, ActionPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IAddCashgamePageModelFactory, AddCashgamePageModelFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameEditPageModelFactory, CashgameEditPageModelFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameChartPageModelFactory, CashgameChartPageModelFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameDetailsPageModelFactory, CashgameDetailsPageModelFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameFactsPageModelFactory, CashgameFactsPageModelFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameLeaderboardPageModelFactory, CashgameLeaderboardPageModelFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameListingPageModelFactory, CashgameListingPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IRunningCashgamePageModelFactory, RunningCashgamePageModelFactory>(container);
            ObjectFactory.RegisterComponent<IAddPlayerPageModelFactory, AddPlayerPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IAddPlayerConfirmationPageModelFactory, AddPlayerConfirmationPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IInvitePlayerPageModelFactory, InvitePlayerPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IInvitePlayerConfirmationPageModelFactory, InvitePlayerConfirmationPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IJoinHomegamePageModelFactory, JoinHomegamePageModelFactory>(container);
            ObjectFactory.RegisterComponent<IJoinHomegameConfirmationPageModelFactory, JoinHomegameConfirmationPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IUserListingPageModelFactory, UserListingPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IAddUserPageModelFactory, AddUserPageModelFactory>(container);
            ObjectFactory.RegisterComponent<IAddUserConfirmationPageModelFactory, AddUserConfirmationPageModelFactory>(container);

            // Model Factories
            ObjectFactory.RegisterComponent<IAvatarModelFactory, AvatarModelFactory>(container);
            ObjectFactory.RegisterComponent<IPagePropertiesFactory, PagePropertiesFactory>(container);
            
            // Validator Factories
            ObjectFactory.RegisterComponent<IUserValidatorFactory, UserValidatorFactory>(container);
            ObjectFactory.RegisterComponent<ICashgameValidatorFactory, CashgameValidatorFactory>(container);
            ObjectFactory.RegisterComponent<IHomegameValidatorFactory, HomegameValidatorFactory>(container);

            // Mappers
            ObjectFactory.RegisterComponent<IHomegameModelMapper, HomegameModelMapper>(container);
            ObjectFactory.RegisterComponent<ICashgameModelMapper, CashgameModelMapper>(container);
            ObjectFactory.RegisterComponent<IUserModelMapper, UserModelMapper>(container);
        }

    }
}