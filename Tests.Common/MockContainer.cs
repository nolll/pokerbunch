﻿using Core.Repositories;
using Core.Services;
using Infrastructure.Caching;
using Infrastructure.Config;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;
using Infrastructure.System;
using Moq;
using NUnit.Framework;
using Web.ModelFactories.AuthModelFactories;
using Web.ModelFactories.CashgameModelFactories;
using Web.ModelFactories.CashgameModelFactories.Buyin;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.PageBaseModelFactories;
using Web.ModelFactories.UserModelFactories;
using Web.Validators;

namespace Tests.Common
{
    public class MockContainer
    {
        protected Mock<IHomegameRepository> HomegameRepositoryMock;
        protected Mock<ICashgameRepository> CashgameRepositoryMock;
        protected Mock<IPlayerRepository> PlayerRepositoryMock;

        protected Mock<IHomegameStorage> HomegameStorageMock;
        protected Mock<IUserStorage> UserStorageMock;
        protected Mock<ICashgameStorage> CashgameStorageMock;
        protected Mock<ICheckpointStorage> CheckpointStorageMock;
        protected Mock<IPlayerStorage> PlayerStorageMock;

        protected Mock<IRawHomegameFactory> RawHomegameFactoryMock;

        protected Mock<ICashgameFactory> CashgameFactoryMock;
        protected Mock<ICashgameSuiteFactory> CashgameSuiteFactoryMock;
        protected Mock<ICashgameResultFactory> CashgameResultFactoryMock;
        protected Mock<IHomegameFactory> HomegameFactoryMock;

        protected Mock<IWebContext> WebContextMock;
        protected Mock<IUserContext> UserContextMock;

        protected Mock<IEncryptionService> EncryptionServiceMock;

        protected Mock<ITimeProvider> TimeProviderMock;

        protected Mock<ICacheProvider> CacheProviderMock;
        protected Mock<ICacheContainer> CacheContainerMock;
        protected CacheContainerFake CacheContainerFake;

        protected Mock<IMatrixPageModelFactory> MatrixPageModelFactoryMock;
        protected Mock<IUserValidatorFactory> UserValidatorFactoryMock;
        protected Mock<IAuthLoginPageModelFactory> AuthLoginPageModelFactoryMock;
        protected Mock<ICashgameValidatorFactory> CashgameValidatorFactoryMock;

        protected Mock<ISettings> SettingsMock;

        protected Mock<IBuyinPageModelFactory> BuyinPageModelFactoryMock;
        protected Mock<IReportPageModelFactory> ReportPageModelFactoryMock;
        protected Mock<ICashoutPageModelFactory> CashoutPageModelFactoryMock;
        protected Mock<IEndPageModelFactory> EndPageModelFactoryMock;
        protected Mock<IActionPageModelFactory> ActionPageModelFactoryMock;
        protected Mock<IAddCashgamePageModelFactory> AddCashgamePageModelFactoryMock;
        protected Mock<ICashgameChartPageModelFactory> CashgameChartPageModelFactoryMock;
        protected Mock<ICashgameDetailsPageModelFactory> CashgameDetailsPageModelFactoryMock;
        protected Mock<ICashgameFactsPageModelFactory> CashgameFactsPageModelFactoryMock;
        protected Mock<ICashgameLeaderboardPageModelFactory> CashgameLeaderboardPageModelFactoryMock;
        protected Mock<ICashgameListingPageModelFactory> CashgameListingPageModelFactoryMock;
        protected Mock<IRunningCashgamePageModelFactory> RunningCashgamePageModelFactoryMock;
        protected Mock<IPagePropertiesFactory> PagePropertiesFactoryMock;
        protected Mock<IUserDetailsPageModelFactory> UserPageModelFactoryMock;

        [SetUp]
        public void SetUpMocks()
        {
            HomegameRepositoryMock = new Mock<IHomegameRepository>();
            CashgameRepositoryMock = new Mock<ICashgameRepository>();
            PlayerRepositoryMock = new Mock<IPlayerRepository>();

            HomegameStorageMock = new Mock<IHomegameStorage>();
            UserStorageMock = new Mock<IUserStorage>();
            CashgameStorageMock = new Mock<ICashgameStorage>();
            CheckpointStorageMock = new Mock<ICheckpointStorage>();
            PlayerStorageMock = new Mock<IPlayerStorage>();

            RawHomegameFactoryMock = new Mock<IRawHomegameFactory>();

            CashgameFactoryMock = new Mock<ICashgameFactory>();
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
            UserValidatorFactoryMock = new Mock<IUserValidatorFactory>();
            AuthLoginPageModelFactoryMock = new Mock<IAuthLoginPageModelFactory>();
            CashgameValidatorFactoryMock = new Mock<ICashgameValidatorFactory>();

            SettingsMock = new Mock<ISettings>();

            BuyinPageModelFactoryMock = new Mock<IBuyinPageModelFactory>();
            ReportPageModelFactoryMock = new Mock<IReportPageModelFactory>();
            CashoutPageModelFactoryMock = new Mock<ICashoutPageModelFactory>();
            EndPageModelFactoryMock = new Mock<IEndPageModelFactory>();
            ActionPageModelFactoryMock = new Mock<IActionPageModelFactory>();
            AddCashgamePageModelFactoryMock = new Mock<IAddCashgamePageModelFactory>();
            CashgameChartPageModelFactoryMock = new Mock<ICashgameChartPageModelFactory>();
            CashgameDetailsPageModelFactoryMock = new Mock<ICashgameDetailsPageModelFactory>();
            CashgameFactsPageModelFactoryMock = new Mock<ICashgameFactsPageModelFactory>();
            CashgameLeaderboardPageModelFactoryMock = new Mock<ICashgameLeaderboardPageModelFactory>();
            CashgameListingPageModelFactoryMock = new Mock<ICashgameListingPageModelFactory>();
            RunningCashgamePageModelFactoryMock = new Mock<IRunningCashgamePageModelFactory>();
            PagePropertiesFactoryMock = new Mock<IPagePropertiesFactory>();
            UserPageModelFactoryMock = new Mock<IUserDetailsPageModelFactory>();
        }

    }
}
