using Core.Repositories;
using Core.Services;
using Infrastructure.Factories;
using Infrastructure.System;
using Moq;
using Web.Commands.AuthCommands;
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
using Web.ModelFactories.CashgameModelFactories.Leaderboard;
using Web.ModelFactories.CashgameModelFactories.Listing;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.CashgameModelFactories.Report;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.ModelMappers;
using Web.ModelServices;
using Web.Services;

namespace Tests.Common
{
    public class WebMocks
    {
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
        public readonly Mock<IGoogleAnalyticsModelFactory> GoogleAnalyticsModelFactoryMock;
        public readonly Mock<IRandomStringGenerator> RandomStringGeneratorMock;
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
        public readonly Mock<ICashgameMatrixTableColumnHeaderModelFactory> CashgameMatrixTableColumnHeaderModelFactoryMock;
        public readonly Mock<ICashgameMatrixTableRowModelFactory> CashgameMatrixTableRowModelFactoryMock;
        public readonly Mock<ICashgameListingTableModelFactory> CashgameListingTableModelFactoryMock; 
        public readonly Mock<ICashgameListingTableItemModelFactory> CashgameListingTableItemModelFactoryMock;
        public readonly Mock<ICashgameLeaderboardTableModelFactory> CashgameLeaderboardTableModelFactoryMock;
        public readonly Mock<ICashgameLeaderboardTableItemModelFactory> CashgameLeaderboardTableItemModelFactoryMock;
        public readonly Mock<ICashgameMatrixTableCellModelFactory> CashgameMatrixTableCellModelFactoryMock;
        public readonly Mock<IResultFormatter> ResultFormatterMock;
        public readonly Mock<IGlobalization> GlobalizationMock;
        public readonly Mock<ICashgameSuiteChartModelFactory> CashgameSuiteChartModelFactoryMock;
        public readonly Mock<IActionChartModelFactory> ActionChartModelFactoryMock;
        public readonly Mock<ICashgameDetailsChartModelFactory> CashgameDetailsChartModelFactoryMock;
        public readonly Mock<IAuthCommandProvider> AuthCommandProviderMock;
        public readonly Mock<IPlayerModelService> PlayerModelServiceMock;
        
        public WebMocks()
        {
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
            GoogleAnalyticsModelFactoryMock = new Mock<IGoogleAnalyticsModelFactory>();
            RandomStringGeneratorMock = new Mock<IRandomStringGenerator>();
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
            CashgameMatrixTableColumnHeaderModelFactoryMock = new Mock<ICashgameMatrixTableColumnHeaderModelFactory>();
            CashgameMatrixTableRowModelFactoryMock = new Mock<ICashgameMatrixTableRowModelFactory>();
            CashgameListingTableModelFactoryMock = new Mock<ICashgameListingTableModelFactory>();
            CashgameListingTableItemModelFactoryMock = new Mock<ICashgameListingTableItemModelFactory>();
            CashgameLeaderboardTableModelFactoryMock = new Mock<ICashgameLeaderboardTableModelFactory>();
            CashgameLeaderboardTableItemModelFactoryMock = new Mock<ICashgameLeaderboardTableItemModelFactory>();
            CashgameMatrixTableCellModelFactoryMock = new Mock<ICashgameMatrixTableCellModelFactory>();
            ResultFormatterMock = new Mock<IResultFormatter>();
            GlobalizationMock = new Mock<IGlobalization>();
            CashgameSuiteChartModelFactoryMock = new Mock<ICashgameSuiteChartModelFactory>();
            ActionChartModelFactoryMock = new Mock<IActionChartModelFactory>();
            CashgameDetailsChartModelFactoryMock = new Mock<ICashgameDetailsChartModelFactory>();
            AuthCommandProviderMock = new Mock<IAuthCommandProvider>();
            PlayerModelServiceMock = new Mock<IPlayerModelService>();
        }

    }

}
