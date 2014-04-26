using Application.Factories;
using Application.Services;
using Application.UseCases.BunchList;
using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameFacts;
using Application.UseCases.CashgameTopList;
using Application.UseCases.PlayerList;
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
using Infrastructure.Integration.Gravatar;
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
            RegisterComponent<IRawHomegameFactory, RawHomegameFactory>();
            RegisterComponent<IRawUserFactory, RawUserFactory>();
            RegisterComponent<IRawCashgameFactory, RawCashgameFactory>();
            RegisterComponent<IRawPlayerFactory, RawPlayerFactory>();
            RegisterComponent<IRawCheckpointFactory, RawCheckpointFactory>();
            RegisterComponent<IRawTwitterCredentialsFactory, RawTwitterCredentialsFactory>();

            // Cache
            RegisterComponent<ICacheProvider, CacheProvider>();
            RegisterComponent<ICacheContainer, CacheContainer>();
            RegisterComponent<ICacheBuster, CacheBuster>();
            RegisterComponent<ICacheKeyProvider, CacheKeyProvider>();

            // System
            RegisterComponent<IGlobalization, Globalization>();
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

            // Data Mappers
            RegisterComponent<ICashgameDataMapper, CashgameDataMapper>();
            RegisterComponent<IHomegameDataMapper, HomegameDataMapper>();
            RegisterComponent<IPlayerDataMapper, PlayerDataMapper>();
            RegisterComponent<ICheckpointDataMapper, CheckpointDataMapper>();
            RegisterComponent<ITwitterCredentialsDataMapper, TwitterCredentialsDataMapper>();
            RegisterComponent<IUserDataMapper, UserDataMapper>();

            // Services
            RegisterComponent<IEncryptionService, EncryptionService>();
            RegisterComponent<IAvatarService, GravatarService>();
            RegisterComponent<IInvitationCodeCreator, InvitationCodeCreator>();
            RegisterComponent<IInvitationSender, InvitationSender>();
            RegisterComponent<IInvitationMessageBuilder, InvitationMessageBuilder>();
            RegisterComponent<IMessageSender, MessageSender>();
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
            RegisterComponent<ICashgameService, CashgameService>();
            RegisterComponent<IResultFormatter, ResultFormatter>();
            RegisterComponent<IResultSharer, ResultSharer>();
            RegisterComponent<ISocialServiceProvider, SocialServiceProvider>();

            // Use Cases
            RegisterComponent<IUserListInteractor, UserListInteractor>();
            RegisterComponent<IBunchListInteractor, BunchListInteractor>();
            RegisterComponent<IPlayerListInteractor, PlayerListInteractor>();
            RegisterComponent<ICashgameTopListInteractor, CashgameTopListInteractor>();
            RegisterComponent<ICashgameContextInteractor, CashgameContextInteractor>();
            RegisterComponent<ICashgameFactsInteractor, CashgameFactsInteractor>();
        }
    }
}