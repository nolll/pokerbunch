using Application.Factories;
using Application.Services;
using Application.UseCases.Actions;
using Application.UseCases.AddBunchForm;
using Application.UseCases.AddCashgame;
using Application.UseCases.AddCashgameForm;
using Application.UseCases.AppContext;
using Application.UseCases.BaseContext;
using Application.UseCases.BunchContext;
using Application.UseCases.BunchList;
using Application.UseCases.Buyin;
using Application.UseCases.BuyinForm;
using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameDetails;
using Application.UseCases.CashgameFacts;
using Application.UseCases.CashgameTopList;
using Application.UseCases.Home;
using Application.UseCases.Login;
using Application.UseCases.LoginForm;
using Application.UseCases.Logout;
using Application.UseCases.PlayerBadges;
using Application.UseCases.PlayerDetails;
using Application.UseCases.PlayerFacts;
using Application.UseCases.PlayerList;
using Application.UseCases.UserDetails;
using Application.UseCases.UserList;
using Castle.Core;
using Castle.Windsor;
using Core.Factories;
using Core.Factories.Interfaces;
using Core.Repositories;
using Core.Services;
using Core.Services.Interfaces;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Mappers;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.SqlServer;
using Infrastructure.Integration.Social;
using Infrastructure.System;
using Infrastructure.Web;

namespace Plumbing
{
    public class ApplicationDependencyResolver : CoreDependencyResolver
    {
        protected ApplicationDependencyResolver(IWindsorContainer container, LifestyleType lifestyleType = LifestyleType.PerWebRequest)
            : base(container, lifestyleType)
        {
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            // Storage
            RegisterComponent<IHomegameStorage, SqlServerHomegameStorage>();
            RegisterComponent<ICashgameStorage, SqlServerCashgameStorage>();
            RegisterComponent<ICheckpointStorage, SqlServerCheckpointStorage>();
            RegisterComponent<IPlayerStorage, SqlServerPlayerStorage>();
            RegisterComponent<IUserStorage, SqlServerUserStorage>();
            RegisterComponent<IStorageProvider, SqlServerStorageProvider>();
            RegisterComponent<ISharingStorage, SqlServerSharingStorage>();
            RegisterComponent<ITwitterStorage, SqlServerTwitterStorage>();

            // Raw Factories
            RegisterComponent<IRawCashgameFactory, RawCashgameFactory>();

            // Cache
            RegisterComponent<ICacheProvider, CacheProvider>();
            RegisterComponent<ICacheContainer, CacheContainer>();
            RegisterComponent<ICacheBuster, CacheBuster>();
            RegisterComponent<ICacheKeyProvider, CacheKeyProvider>();

            // System
            RegisterComponent<IWebContext, WebContext>();
            RegisterComponent<ITimeProvider, TimeProvider>();
            RegisterComponent<ISettings, Settings>();

            // Repositories
            RegisterComponent<IHomegameRepository, HomegameRepository>();
            RegisterComponent<ICashgameRepository, CashgameRepository>();
            RegisterComponent<IPlayerRepository, PlayerRepository>();
            RegisterComponent<IUserRepository, UserRepository>();
            RegisterComponent<ITwitterRepository, TwitterRepository>();
            RegisterComponent<ISharingRepository, SharingRepository>();
            RegisterComponent<ICheckpointRepository, CheckpointRepository>();

            // Core Factories
            RegisterComponent<ICashgameResultFactory, CashgameResultFactory>();
            RegisterComponent<ICashgameTotalResultFactory, CashgameTotalResultFactory>();
            RegisterComponent<ICashgameSuiteFactory, CashgameSuiteFactory>();

            // Data Mappers
            RegisterComponent<ICashgameDataMapper, CashgameDataMapper>();
            RegisterComponent<IPlayerDataMapper, PlayerDataMapper>();

            // Services
            RegisterComponent<IInvitationSender, InvitationSender>();
            RegisterComponent<IMessageSender, MessageSender>();
            RegisterComponent<IUserService, UserService>();
            RegisterComponent<IRegistrationConfirmationSender, RegistrationConfirmationSender>();
            RegisterComponent<IPasswordSender, PasswordSender>();
            RegisterComponent<ITwitterIntegration, TwitterIntegration>();
            RegisterComponent<ICashgameService, CashgameService>();
            RegisterComponent<IResultSharer, ResultSharer>();
            RegisterComponent<ISocialServiceProvider, SocialServiceProvider>();

            // Use Cases
            RegisterComponent<IBaseContextInteractor, BaseContextInteractor>();
            RegisterComponent<IAppContextInteractor, AppContextInteractor>();
            RegisterComponent<IBunchContextInteractor, BunchContextInteractor>();
            RegisterComponent<ICashgameContextInteractor, CashgameContextInteractor>();
            RegisterComponent<IUserListInteractor, UserListInteractor>();
            RegisterComponent<IBunchListInteractor, BunchListInteractor>();
            RegisterComponent<IPlayerListInteractor, PlayerListInteractor>();
            RegisterComponent<ITopListInteractor, TopListInteractor>();
            RegisterComponent<ICashgameFactsInteractor, CashgameFactsInteractor>();
            RegisterComponent<IActionsInteractor, ActionsInteractor>();
            RegisterComponent<IAddCashgameFormInteractor, AddCashgameFormInteractor>();
            RegisterComponent<IPlayerDetailsInteractor, PlayerDetailsInteractor>();
            RegisterComponent<IPlayerFactsInteractor, PlayerFactsInteractor>();
            RegisterComponent<IPlayerBadgesInteractor, PlayerBadgesInteractor>();
            RegisterComponent<IUserDetailsInteractor, UserDetailsInteractor>();
            RegisterComponent<ICashgameDetailsInteractor, CashgameDetailsInteractor>();
            RegisterComponent<IAddCashgameInteractor, AddCashgameInteractor>();
            RegisterComponent<IBuyinFormInteractor, BuyinFormInteractor>();
            RegisterComponent<IBuyinInteractor, BuyinInteractor>();
            RegisterComponent<ILoginFormInteractor, LoginFormInteractor>();
            RegisterComponent<ILoginInteractor, LoginInteractor>();
            RegisterComponent<ILogoutInteractor, LogoutInteractor>();
            RegisterComponent<IHomeInteractor, HomeInteractor>();
            RegisterComponent<IAddBunchFormInteractor, AddBunchFormInteractor>();
        }
    }
}