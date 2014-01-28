﻿using Application.Factories;
using Application.Factories.Interfaces;
using Application.Services;
using Application.Services.Interfaces;
using Castle.Core;
using Castle.Windsor;
using Core.Repositories;
using Infrastructure.Factories;
using Infrastructure.Integration.Gravatar;
using Infrastructure.Integration.Twitter;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.System;
using Plumbing;
using Web.Commands.AuthCommands;
using Web.Commands.CashgameCommands;
using Web.Commands.HomegameCommands;
using Web.Commands.PlayerCommands;
using Web.Commands.SharingCommands;
using Web.Commands.UserCommands;
using Web.ModelFactories.AuthModelFactories;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.ModelFactories.CashgameModelFactories.Add;
using Web.ModelFactories.CashgameModelFactories.Buyin;
using Web.ModelFactories.CashgameModelFactories.Cashout;
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.ModelFactories.CashgameModelFactories.End;
using Web.ModelFactories.CashgameModelFactories.Facts;
using Web.ModelFactories.CashgameModelFactories.List;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.CashgameModelFactories.Report;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.ModelFactories.CashgameModelFactories.Toplist;
using Web.ModelFactories.ChartModelFactories;
using Web.ModelFactories.HomeModelFactories;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.ModelFactories.PlayerModelFactories;
using Web.ModelFactories.SharingModelFactories;
using Web.ModelFactories.UserModelFactories;
using Web.ModelMappers;
using Web.ModelServices;
using Web.Services;
using CashgameService = Application.Services.CashgameService;

namespace Web.Plumbing
{
    public class WebDependencyResolver : InfrastructureDependencyResolver
    {
        public WebDependencyResolver(IWindsorContainer container, LifestyleType lifestyleType = LifestyleType.PerWebRequest)
            : base(container, lifestyleType)
        {
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            // Services
            RegisterComponent<IEncryptionService, EncryptionService>();
            RegisterComponent<IAvatarService, GravatarService>();
            RegisterComponent<IInvitationCodeCreator, InvitationCodeCreator>();
            RegisterComponent<IInvitationSender, InvitationSender>();
            RegisterComponent<IInvitationMessageBuilder, InvitationMessageBuilder>();
            RegisterComponent<IMessageSender, MessageSender>();
            RegisterComponent<IUrlProvider, UrlProvider>();
            RegisterComponent<IUserService, UserService>();
            RegisterComponent<IPasswordGenerator, PasswordGenerator>();
            RegisterComponent<ISaltGenerator, SaltGenerator>();
            RegisterComponent<IRegistrationConfirmationSender, RegistrationConfirmationSender>();
            RegisterComponent<IRegistrationConfirmationMessageBuilder, RegistrationConfirmationMessageBuilder>();
            RegisterComponent<ISlugGenerator, SlugGenerator>();
            RegisterComponent<IPasswordSender, PasswordSender>();
            RegisterComponent<IPasswordMessageBuilder, PasswordMessageBuilder>();
            RegisterComponent<ITwitterIntegration, TwitterIntegration>();
            RegisterComponent<IRandomStringGenerator, RandomStringGenerator>();
            RegisterComponent<IResultFormatter, ResultFormatter>();
            RegisterComponent<ICashgameService, CashgameService>();

            // System
            RegisterComponent<IWebContext, WebContext>();
            RegisterComponent<ITimeProvider, TimeProvider>();
            RegisterComponent<ISettings, Settings>();

            // Core Factories
            RegisterComponent<IHomegameFactory, HomegameFactory>();
            RegisterComponent<IUserFactory, UserFactory>();
            RegisterComponent<ICashgameFactory, CashgameFactory>();
            RegisterComponent<IPlayerFactory, PlayerFactory>();
            RegisterComponent<ICashgameResultFactory, CashgameResultFactory>();
            RegisterComponent<ICashgameTotalResultFactory, CashgameTotalResultFactory>();
            RegisterComponent<ICashgameSuiteFactory, CashgameSuiteFactory>();
            RegisterComponent<ICashgameFactsFactory, CashgameFactsFactory>();
            RegisterComponent<ICheckpointFactory, CheckpointFactory>();
            RegisterComponent<ITwitterCredentialsFactory, TwitterCredentialsFactory>();

            // Model Services
            RegisterComponent<IHomeModelService, HomeModelService>();
            RegisterComponent<IAuthModelService, AuthModelService>();
            RegisterComponent<IHomegameModelService, HomegameModelService>();
            RegisterComponent<IPlayerModelService, PlayerModelService>();
            RegisterComponent<ICashgameModelService, CashgameModelService>();
            RegisterComponent<IUserModelService, UserModelService>();
            RegisterComponent<ISharingModelService, SharingModelService>();

            // Page Model Factories
            RegisterComponent<IHomePageModelFactory, HomePageModelFactory>();
            RegisterComponent<IMatrixPageModelFactory, MatrixPageModelFactory>();
            RegisterComponent<IAuthLoginPageModelFactory, AuthLoginPageModelFactory>();
            RegisterComponent<IAddHomegamePageModelFactory, AddHomegamePageModelFactory>();
            RegisterComponent<IAddHomegameConfirmationPageModelFactory, AddHomegameConfirmationPageModelFactory>();
            RegisterComponent<IBuyinPageModelFactory, BuyinPageModelFactory>();
            RegisterComponent<IReportPageModelFactory, ReportPageModelFactory>();
            RegisterComponent<ICashoutPageModelFactory, CashoutPageModelFactory>();
            RegisterComponent<IEndPageModelFactory, EndPageModelFactory>();
            RegisterComponent<IUserDetailsPageModelFactory, UserDetailsPageModelFactory>();
            RegisterComponent<IPlayerListPageModelFactory, PlayerListPageModelFactory>();
            RegisterComponent<IPlayerDetailsPageModelFactory, PlayerDetailsPageModelFactory>();
            RegisterComponent<IHomegameListPageModelFactory, HomegameListPageModelFactory>();
            RegisterComponent<IHomegameDetailsPageModelFactory, HomegameDetailsPageModelFactory>();
            RegisterComponent<IHomegameEditPageModelFactory, HomegameEditPageModelFactory>();
            RegisterComponent<IActionPageModelFactory, ActionPageModelFactory>();
            RegisterComponent<IAddCashgamePageModelFactory, AddCashgamePageModelFactory>();
            RegisterComponent<ICashgameEditPageModelFactory, CashgameEditPageModelFactory>();
            RegisterComponent<ICashgameChartPageModelFactory, CashgameChartPageModelFactory>();
            RegisterComponent<ICashgameDetailsPageModelFactory, CashgameDetailsPageModelFactory>();
            RegisterComponent<ICashgameFactsPageModelFactory, CashgameFactsPageModelFactory>();
            RegisterComponent<ICashgameToplistPageModelFactory, CashgameToplistPageModelFactory>();
            RegisterComponent<ICashgameListPageModelFactory, CashgameListPageModelFactory>();
            RegisterComponent<IRunningCashgamePageModelFactory, RunningCashgamePageModelFactory>();
            RegisterComponent<IAddPlayerPageModelFactory, AddPlayerPageModelFactory>();
            RegisterComponent<IAddPlayerConfirmationPageModelFactory, AddPlayerConfirmationPageModelFactory>();
            RegisterComponent<IInvitePlayerPageModelFactory, InvitePlayerPageModelFactory>();
            RegisterComponent<IInvitePlayerConfirmationPageModelFactory, InvitePlayerConfirmationPageModelFactory>();
            RegisterComponent<IJoinHomegamePageModelFactory, JoinHomegamePageModelFactory>();
            RegisterComponent<IJoinHomegameConfirmationPageModelFactory, JoinHomegameConfirmationPageModelFactory>();
            RegisterComponent<IUserListPageModelFactory, UserListPageModelFactory>();
            RegisterComponent<IAddUserPageModelFactory, AddUserPageModelFactory>();
            RegisterComponent<IAddUserConfirmationPageModelFactory, AddUserConfirmationPageModelFactory>();
            RegisterComponent<IEditUserPageModelFactory, EditUserPageModelFactory>();
            RegisterComponent<IChangePasswordPageModelFactory, ChangePasswordPageModelFactory>();
            RegisterComponent<IForgotPasswordPageModelFactory, ForgotPasswordPageModelFactory>();
            RegisterComponent<ISharingIndexPageModelFactory, SharingIndexPageModelFactory>();
            RegisterComponent<ISharingTwitterPageModelFactory, SharingTwitterPageModelFactory>();
            RegisterComponent<ICashgameListTableModelFactory, CashgameListTableModelFactory>();
            RegisterComponent<ICashgameListTableItemModelFactory, CashgameListTableItemModelFactory>();

            // Model Factories
            RegisterComponent<IAvatarModelFactory, AvatarModelFactory>();
            RegisterComponent<IPagePropertiesFactory, PagePropertiesFactory>();
            RegisterComponent<IGoogleAnalyticsModelFactory, GoogleAnalyticsModelFactory>();
            RegisterComponent<IHomegameNavigationModelFactory, HomegameNavigationModelFactory>();
            RegisterComponent<IUserNavigationModelFactory, UserNavigationModelFactory>();
            RegisterComponent<IAdminNavigationModelFactory, AdminNavigationModelFactory>();
            RegisterComponent<ICashgamePageNavigationModelFactory, CashgamePageNavigationModelFactory>();
            RegisterComponent<ICashgameDetailsTableModelFactory, CashgameDetailsTableModelFactory>();
            RegisterComponent<ICashgameDetailsTableItemModelFactory, CashgameDetailsTableItemModelFactory>();
            RegisterComponent<ICheckpointModelFactory, CheckpointModelFactory>();
            RegisterComponent<ICashgameMatrixTableModelFactory, CashgameMatrixTableModelFactory>();
            RegisterComponent<ICashgameMatrixTableColumnHeaderModelFactory, CashgameMatrixTableColumnHeaderModelFactory>();
            RegisterComponent<IRunningCashgameTableModelFactory, RunningCashgameTableModelFactory>();
            RegisterComponent<IRunningCashgameTableItemModelFactory, RunningCashgameTableItemModelFactory>();
            RegisterComponent<ICashgameYearNavigationModelFactory, CashgameYearNavigationModelFactory>();
            RegisterComponent<IHomegameListItemModelFactory, HomegameListItemModelFactory>();
            RegisterComponent<ICashgameToplistTableModelFactory, CashgameToplistTableModelFactory>();
            RegisterComponent<ICashgameToplistTableItemModelFactory, CashgameToplistTableItemModelFactory>();
            RegisterComponent<IUserItemModelFactory, UserItemModelFactory>();
            RegisterComponent<IBarModelFactory, BarModelFactory>();
            RegisterComponent<ICashgameMatrixTableRowModelFactory, CashgameMatrixTableRowModelFactory>();
            RegisterComponent<IPlayerItemModelFactory, PlayerItemModelFactory>();
            RegisterComponent<ICashgameMatrixTableCellModelFactory, CashgameMatrixTableCellModelFactory>();
            RegisterComponent<IPlayerFactsModelFactory, PlayerFactsModelFactory>();
            RegisterComponent<ICashgameSuiteChartModelFactory, CashgameSuiteChartModelFactory>();
            RegisterComponent<IActionChartModelFactory, ActionChartModelFactory>();
            RegisterComponent<ICashgameDetailsChartModelFactory, CashgameDetailsChartModelFactory>();
            RegisterComponent<IChartValueModelFactory, ChartValueModelFactory>();

            // Mappers
            RegisterComponent<IHomegameModelMapper, HomegameModelMapper>();
            RegisterComponent<ICashgameModelMapper, CashgameModelMapper>();
            RegisterComponent<IUserModelMapper, UserModelMapper>();
            RegisterComponent<ICheckpointModelMapper, CheckpointModelMapper>();

            // Command Providers
            RegisterComponent<IPlayerCommandProvider, PlayerCommandProvider>();
            RegisterComponent<IAuthCommandProvider, AuthCommandProvider>();
            RegisterComponent<IUserCommandProvider, UserCommandProvider>();
            RegisterComponent<IHomegameCommandProvider, HomegameCommandProvider>();
            RegisterComponent<ICashgameCommandProvider, CashgameCommandProvider>();
            RegisterComponent<ISharingCommandProvider, SharingCommandProvider>();
        }

    }
}