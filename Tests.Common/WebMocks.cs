using Core.Repositories;
using Core.Services;
using Infrastructure.Caching;
using Infrastructure.Data.Factories;
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
        public readonly Mock<IHomegameRepository> HomegameRepositoryMock;
        public readonly Mock<ICashgameRepository> CashgameRepositoryMock;
        public readonly Mock<IPlayerRepository> PlayerRepositoryMock;
        public readonly Mock<IUserRepository> UserRepositoryMock;
        public readonly Mock<ICashgameFactory> CashgameFactoryMock;
        public readonly Mock<ICheckpointFactory> CheckpointFactoryMock;
        public readonly Mock<ICashgameSuiteFactory> CashgameSuiteFactoryMock;
        public readonly Mock<ICashgameResultFactory> CashgameResultFactoryMock;
        public readonly Mock<IHomegameFactory> HomegameFactoryMock;
        public readonly Mock<IWebContext> WebContextMock;
        public readonly Mock<IUserContext> UserContextMock;
        public readonly Mock<IEncryptionService> EncryptionServiceMock;
        public readonly Mock<ITimeProvider> TimeProviderMock;
        public readonly Mock<ICacheProvider> CacheProviderMock;
        public readonly Mock<ICacheContainer> CacheContainerMock;
        public readonly CacheContainerFake CacheContainerFake;
        public readonly Mock<IMatrixPageModelFactory> MatrixPageModelFactoryMock;
        public readonly Mock<IAuthLoginPageModelFactory> AuthLoginPageModelFactoryMock;
        public readonly Mock<ISettings> SettingsMock;
        public readonly Mock<IBuyinPageModelFactory> BuyinPageModelFactoryMock;
        public readonly Mock<IReportPageModelFactory> ReportPageModelFactoryMock;
        public readonly Mock<ICashoutPageModelFactory> CashoutPageModelFactoryMock;
        public readonly Mock<IEndPageModelFactory> EndPageModelFactoryMock;
        public readonly Mock<IActionPageModelFactory> ActionPageModelFactoryMock;
        public readonly Mock<IAddCashgamePageModelFactory> AddCashgamePageModelFactoryMock;
        public readonly Mock<ICashgameChartPageModelFactory> CashgameChartPageModelFactoryMock;
        public readonly Mock<ICashgameDetailsPageModelFactory> CashgameDetailsPageModelFactoryMock;
        public readonly Mock<ICashgameEditPageModelFactory> CashgameEditPageModelFactoryMock;
        public readonly Mock<ICashgameFactsPageModelFactory> CashgameFactsPageModelFactoryMock;
        public readonly Mock<ICashgameLeaderboardPageModelFactory> CashgameLeaderboardPageModelFactoryMock;
        public readonly Mock<ICashgameListingPageModelFactory> CashgameListingPageModelFactoryMock;
        public readonly Mock<IRunningCashgamePageModelFactory> RunningCashgamePageModelFactoryMock;
        public readonly Mock<ICashgameModelMapper> CashgameModelMapperMock;
        public readonly Mock<IPagePropertiesFactory> PagePropertiesFactoryMock;
        public readonly Mock<IUserDetailsPageModelFactory> UserPageModelFactoryMock;
        public readonly Mock<IGoogleAnalyticsModelFactory> GoogleAnalyticsModelFactoryMock;
        public readonly Mock<IRandomStringGenerator> RandomStringGeneratorMock;
        public readonly Mock<IAvatarModelFactory> AvatarModelFactoryMock;
        public readonly Mock<IHomegameNavigationModelFactory> HomegameNavigationModelFactoryMock;
        public readonly Mock<IUserNavigationModelFactory> UserNavigationModelFactoryMock;
        public readonly Mock<ICheckpointRepository> CheckpointRepositoryMock;
        public readonly Mock<ICheckpointModelMapper> CheckpointModelMapperMock;
        public readonly Mock<IAdminNavigationModelFactory> AdminNavigationModelFactoryMock;
        public readonly Mock<IUrlProvider> UrlProviderMock;
        public readonly Mock<ICashgameDetailsTableModelFactory> CashgameDetailsTableModelFactoryMock;
        public readonly Mock<ICashgameDetailsTableItemModelFactory> CashgameDetailsTableItemModelFactoryMock;
        public readonly Mock<IRunningCashgameTableModelFactory> RunningCashgameTableModelFactoryMock;
        public readonly Mock<IRunningCashgameTableItemModelFactory> RunningCashgameTableItemModelFactoryMock;
        public readonly Mock<ICashgameNavigationModelFactory> CashgameNavigationModelFactoryMock;
        public readonly Mock<ICheckpointModelFactory> CheckpointModelFactoryMock;

        public WebMocks()
        {
            HomegameRepositoryMock = new Mock<IHomegameRepository>();
            CashgameRepositoryMock = new Mock<ICashgameRepository>();
            PlayerRepositoryMock = new Mock<IPlayerRepository>();
            UserRepositoryMock = new Mock<IUserRepository>();
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
            CheckpointModelMapperMock = new Mock<ICheckpointModelMapper>();
            AdminNavigationModelFactoryMock = new Mock<IAdminNavigationModelFactory>();
            UrlProviderMock = new Mock<IUrlProvider>();
            CashgameDetailsTableModelFactoryMock = new Mock<ICashgameDetailsTableModelFactory>();
            CashgameDetailsTableItemModelFactoryMock = new Mock<ICashgameDetailsTableItemModelFactory>();
            RunningCashgameTableModelFactoryMock = new Mock<IRunningCashgameTableModelFactory>();
            RunningCashgameTableItemModelFactoryMock = new Mock<IRunningCashgameTableItemModelFactory>();
            CashgameNavigationModelFactoryMock = new Mock<ICashgameNavigationModelFactory>();
            CheckpointModelFactoryMock = new Mock<ICheckpointModelFactory>();
        }

    }

}
