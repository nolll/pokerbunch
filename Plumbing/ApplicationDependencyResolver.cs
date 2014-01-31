using Application.Factories;
using Application.Factories.Interfaces;
using Application.Services;
using Castle.Core;
using Castle.Windsor;
using Core.Repositories;
using Infrastructure.Caching;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage;
using Infrastructure.Factories;
using Infrastructure.Integration.Gravatar;
using Infrastructure.Integration.Twitter;
using Infrastructure.Repositories;
using Infrastructure.System;
using Infrastructure.Web;

namespace Plumbing
{
    public class ApplicationDependencyResolver : DependencyResolver
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
            RegisterComponent<IAuthentication, Authentication>();
            RegisterComponent<IAuthorization, Authorization>();

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
        }
    }
}