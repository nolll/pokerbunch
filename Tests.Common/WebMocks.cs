﻿using Core.Repositories;
using Core.Services;
using Infrastructure.Caching;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;
using Infrastructure.System;
using Moq;
using Web.ModelFactories.AuthModelFactories;
using Web.ModelFactories.CashgameModelFactories;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.ModelFactories.UserModelFactories;
using Web.ModelMappers;

namespace Tests.Common
{
    public class WebMocks
    {
        public Mock<IHomegameRepository> HomegameRepositoryMock;
        public Mock<ICashgameRepository> CashgameRepositoryMock;
        public Mock<IPlayerRepository> PlayerRepositoryMock;
        public Mock<IUserRepository> UserRepositoryMock;
        public Mock<IHomegameStorage> HomegameStorageMock;
        public Mock<IUserStorage> UserStorageMock;
        public Mock<ICashgameStorage> CashgameStorageMock;
        public Mock<ICheckpointStorage> CheckpointStorageMock;
        public Mock<IPlayerStorage> PlayerStorageMock;
        public Mock<IRawHomegameFactory> RawHomegameFactoryMock;
        public Mock<ICashgameFactory> CashgameFactoryMock;
        public Mock<ICheckpointFactory> CheckpointFactoryMock;
        public Mock<ICashgameSuiteFactory> CashgameSuiteFactoryMock;
        public Mock<ICashgameResultFactory> CashgameResultFactoryMock;
        public Mock<IHomegameFactory> HomegameFactoryMock;
        public Mock<IWebContext> WebContextMock;
        public Mock<IUserContext> UserContextMock;
        public Mock<IEncryptionService> EncryptionServiceMock;
        public Mock<ITimeProvider> TimeProviderMock;
        public Mock<ICacheProvider> CacheProviderMock;
        public Mock<ICacheContainer> CacheContainerMock;
        public CacheContainerFake CacheContainerFake;
        public Mock<IMatrixPageModelFactory> MatrixPageModelFactoryMock;
        public Mock<IAuthLoginPageModelFactory> AuthLoginPageModelFactoryMock;
        public Mock<ISettings> SettingsMock;
        public Mock<IBuyinPageModelFactory> BuyinPageModelFactoryMock;
        public Mock<IReportPageModelFactory> ReportPageModelFactoryMock;
        public Mock<ICashoutPageModelFactory> CashoutPageModelFactoryMock;
        public Mock<IEndPageModelFactory> EndPageModelFactoryMock;
        public Mock<IActionPageModelFactory> ActionPageModelFactoryMock;
        public Mock<IAddCashgamePageModelFactory> AddCashgamePageModelFactoryMock;
        public Mock<ICashgameChartPageModelFactory> CashgameChartPageModelFactoryMock;
        public Mock<ICashgameDetailsPageModelFactory> CashgameDetailsPageModelFactoryMock;
        public Mock<ICashgameEditPageModelFactory> CashgameEditPageModelFactoryMock;
        public Mock<ICashgameFactsPageModelFactory> CashgameFactsPageModelFactoryMock;
        public Mock<ICashgameLeaderboardPageModelFactory> CashgameLeaderboardPageModelFactoryMock;
        public Mock<ICashgameListingPageModelFactory> CashgameListingPageModelFactoryMock;
        public Mock<IRunningCashgamePageModelFactory> RunningCashgamePageModelFactoryMock;
        public Mock<ICashgameModelMapper> CashgameModelMapperMock;
        public Mock<IPagePropertiesFactory> PagePropertiesFactoryMock;
        public Mock<IUserDetailsPageModelFactory> UserPageModelFactoryMock;
        public Mock<IGoogleAnalyticsModelFactory> GoogleAnalyticsModelFactoryMock;
        public Mock<IRandomStringGenerator> RandomStringGeneratorMock;
        public Mock<IAvatarModelFactory> AvatarModelFactoryMock;
        public Mock<IHomegameNavigationModelFactory> HomegameNavigationModelFactoryMock;
        public Mock<IUserNavigationModelFactory> UserNavigationModelFactoryMock;
        public Mock<ICheckpointRepository> CheckpointRepositoryMock;
        public Mock<IRawCashgameFactory> RawCashgameFactoryMock;
        public Mock<ICheckpointModelMapper> CheckpointModelMapperMock;
        public Mock<IAdminNavigationModelFactory> AdminNavigationModelFactoryMock;

        public WebMocks()
        {
            HomegameRepositoryMock = new Mock<IHomegameRepository>();
            CashgameRepositoryMock = new Mock<ICashgameRepository>();
            PlayerRepositoryMock = new Mock<IPlayerRepository>();
            UserRepositoryMock = new Mock<IUserRepository>();
            HomegameStorageMock = new Mock<IHomegameStorage>();
            UserStorageMock = new Mock<IUserStorage>();
            CashgameStorageMock = new Mock<ICashgameStorage>();
            CheckpointStorageMock = new Mock<ICheckpointStorage>();
            PlayerStorageMock = new Mock<IPlayerStorage>();
            RawHomegameFactoryMock = new Mock<IRawHomegameFactory>();
            CashgameFactoryMock = new Mock<ICashgameFactory>();
            CheckpointFactoryMock = new Mock<ICheckpointFactory>();
            CashgameSuiteFactoryMock = new Mock<ICashgameSuiteFactory>();
            CashgameResultFactoryMock = new Mock<ICashgameResultFactory>();
            HomegameFactoryMock = new Mock<IHomegameFactory>();
            WebContextMock = new Mock<IWebContext>();
            UserContextMock = new Mock<IUserContext>();
            EncryptionServiceMock = new Mock<IEncryptionService>();
            TimeProviderMock = new Mock<ITimeProvider>();
            CacheProviderMock = new Mock<ICacheProvider>();
            CacheContainerMock = new Mock<ICacheContainer>();
            CacheContainerFake = new CacheContainerFake();
            MatrixPageModelFactoryMock = new Mock<IMatrixPageModelFactory>();
            AuthLoginPageModelFactoryMock = new Mock<IAuthLoginPageModelFactory>();
            SettingsMock = new Mock<ISettings>();
            BuyinPageModelFactoryMock = new Mock<IBuyinPageModelFactory>();
            ReportPageModelFactoryMock = new Mock<IReportPageModelFactory>();
            CashoutPageModelFactoryMock = new Mock<ICashoutPageModelFactory>();
            EndPageModelFactoryMock = new Mock<IEndPageModelFactory>();
            ActionPageModelFactoryMock = new Mock<IActionPageModelFactory>();
            AddCashgamePageModelFactoryMock = new Mock<IAddCashgamePageModelFactory>();
            CashgameChartPageModelFactoryMock = new Mock<ICashgameChartPageModelFactory>();
            CashgameDetailsPageModelFactoryMock = new Mock<ICashgameDetailsPageModelFactory>();
            CashgameEditPageModelFactoryMock = new Mock<ICashgameEditPageModelFactory>();
            CashgameFactsPageModelFactoryMock = new Mock<ICashgameFactsPageModelFactory>();
            CashgameLeaderboardPageModelFactoryMock = new Mock<ICashgameLeaderboardPageModelFactory>();
            CashgameListingPageModelFactoryMock = new Mock<ICashgameListingPageModelFactory>();
            RunningCashgamePageModelFactoryMock = new Mock<IRunningCashgamePageModelFactory>();
            CashgameModelMapperMock = new Mock<ICashgameModelMapper>();
            PagePropertiesFactoryMock = new Mock<IPagePropertiesFactory>();
            UserPageModelFactoryMock = new Mock<IUserDetailsPageModelFactory>();
            GoogleAnalyticsModelFactoryMock = new Mock<IGoogleAnalyticsModelFactory>();
            RandomStringGeneratorMock = new Mock<IRandomStringGenerator>();
            AvatarModelFactoryMock = new Mock<IAvatarModelFactory>();
            HomegameNavigationModelFactoryMock = new Mock<IHomegameNavigationModelFactory>();
            UserNavigationModelFactoryMock = new Mock<IUserNavigationModelFactory>();
            CheckpointRepositoryMock = new Mock<ICheckpointRepository>();
            RawCashgameFactoryMock = new Mock<IRawCashgameFactory>();
            CheckpointModelMapperMock = new Mock<ICheckpointModelMapper>();
            AdminNavigationModelFactoryMock = new Mock<IAdminNavigationModelFactory>();
        }

    }

}
