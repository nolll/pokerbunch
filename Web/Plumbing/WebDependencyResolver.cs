using Application.Services;
using Castle.Core;
using Castle.Windsor;
using Plumbing;
using Web.Commands.AdminCommands;
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
using Web.ModelFactories.CashgameModelFactories.Checkpoints;
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
using Web.Models.PlayerModels.Badges;
using Web.Security;
using Web.Services;

namespace Web.Plumbing
{
    public class WebDependencyResolver : ApplicationDependencyResolver
    {
        public WebDependencyResolver(IWindsorContainer container, LifestyleType lifestyleType = LifestyleType.PerWebRequest)
            : base(container, lifestyleType)
        {
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            // Services
            RegisterComponent<IConfigService, ConfigService>();
            RegisterComponent<IUrlProvider, UrlProvider>();

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
            RegisterComponent<IBunchListPageBuilder, BunchListPageBuilder>();
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
            RegisterComponent<IUserListPageBuilder, UserListPageBuilder>();
            RegisterComponent<IAddUserPageModelFactory, AddUserPageModelFactory>();
            RegisterComponent<IAddUserConfirmationPageModelFactory, AddUserConfirmationPageModelFactory>();
            RegisterComponent<IEditUserPageModelFactory, EditUserPageModelFactory>();
            RegisterComponent<IChangePasswordPageModelFactory, ChangePasswordPageModelFactory>();
            RegisterComponent<IForgotPasswordPageModelFactory, ForgotPasswordPageModelFactory>();
            RegisterComponent<ISharingIndexPageModelFactory, SharingIndexPageModelFactory>();
            RegisterComponent<ISharingTwitterPageModelFactory, SharingTwitterPageModelFactory>();
            RegisterComponent<ICashgameListTableModelFactory, CashgameListTableModelFactory>();
            RegisterComponent<ICashgameListTableItemModelFactory, CashgameListTableItemModelFactory>();
            RegisterComponent<IEditCheckpointPageModelFactory, EditCheckpointPageModelFactory>();

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
            RegisterComponent<IBunchListItemModelFactory, BunchListItemModelFactory>();
            RegisterComponent<ICashgameToplistTableModelFactory, CashgameToplistTableModelFactory>();
            RegisterComponent<ICashgameToplistTableItemModelFactory, CashgameToplistTableItemModelFactory>();
            RegisterComponent<IUserListItemModelFactory, UserListItemModelFactory>();
            RegisterComponent<IBarModelFactory, BarModelFactory>();
            RegisterComponent<ICashgameMatrixTableRowModelFactory, CashgameMatrixTableRowModelFactory>();
            RegisterComponent<IPlayerItemModelFactory, PlayerItemModelFactory>();
            RegisterComponent<ICashgameMatrixTableCellModelFactory, CashgameMatrixTableCellModelFactory>();
            RegisterComponent<IPlayerFactsModelFactory, PlayerFactsModelFactory>();
            RegisterComponent<ICashgameSuiteChartModelFactory, CashgameSuiteChartModelFactory>();
            RegisterComponent<IActionChartModelFactory, ActionChartModelFactory>();
            RegisterComponent<ICashgameDetailsChartModelFactory, CashgameDetailsChartModelFactory>();
            RegisterComponent<IChartValueModelFactory, ChartValueModelFactory>();
            RegisterComponent<IPlayerBadgesModelFactory, PlayerBadgesModelFactory>();
            RegisterComponent<IBadgeModelFactory, BadgeModelFactory>();

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
            RegisterComponent<IAdminCommandProvider, AdminCommandProvider>();

            // Security
            RegisterComponent<IAuth, Auth>();
        }

    }
}