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
using Web.ModelFactories.PlayerModelFactories;
using Web.ModelFactories.SharingModelFactories;
using Web.ModelFactories.UserModelFactories;
using Web.ModelMappers;
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

            // Page Model Factories
            RegisterComponent<IHomePageBuilder, HomePageBuilder>();
            RegisterComponent<IMatrixPageBuilder, MatrixPageBuilder>();
            RegisterComponent<ILoginPageBuilder, LoginPageBuilder>();
            RegisterComponent<IAddHomegamePageBuilder, AddHomegamePageBuilder>();
            RegisterComponent<IAddHomegameConfirmationPageBuilder, AddHomegameConfirmationPageBuilder>();
            RegisterComponent<IBuyinPageBuilder, BuyinPageBuilder>();
            RegisterComponent<IReportPageBuilder, ReportPageBuilder>();
            RegisterComponent<ICashoutPageBuilder, CashoutPageBuilder>();
            RegisterComponent<IEndPageBuilder, EndPageBuilder>();
            RegisterComponent<IUserDetailsPageBuilder, UserDetailsPageBuilder>();
            RegisterComponent<IPlayerListPageBuilder, PlayerListPageBuilder>();
            RegisterComponent<IPlayerDetailsPageBuilder, PlayerDetailsPageBuilder>();
            RegisterComponent<IBunchListPageBuilder, BunchListPageBuilder>();
            RegisterComponent<IHomegameDetailsPageBuilder, HomegameDetailsPageBuilder>();
            RegisterComponent<IEditHomegamePageBuilder, EditHomegamePageBuilder>();
            RegisterComponent<IActionPageBuilder, ActionPageBuilder>();
            RegisterComponent<IAddCashgamePageBuilder, AddCashgamePageBuilder>();
            RegisterComponent<IEditCashgamePageBuilder, EditCashgamePageBuilder>();
            RegisterComponent<ICashgameChartPageBuilder, CashgameChartPageBuilder>();
            RegisterComponent<ICashgameDetailsPageBuilder, CashgameDetailsPageBuilder>();
            RegisterComponent<ICashgameFactsPageBuilder, CashgameFactsPageBuilder>();
            RegisterComponent<IToplistPageBuilder, ToplistPageBuilder>();
            RegisterComponent<ICashgameListPageBuilder, CashgameListPageBuilder>();
            RegisterComponent<IRunningCashgamePageBuilder, RunningCashgamePageBuilder>();
            RegisterComponent<IAddPlayerPageBuilder, AddPlayerPageBuilder>();
            RegisterComponent<IAddPlayerConfirmationPageBuilder, AddPlayerConfirmationPageBuilder>();
            RegisterComponent<IInvitePlayerPageBuilder, InvitePlayerPageBuilder>();
            RegisterComponent<IInvitePlayerConfirmationPageBuilder, InvitePlayerConfirmationPageBuilder>();
            RegisterComponent<IJoinHomegamePageBuilder, JoinHomegamePageBuilder>();
            RegisterComponent<IJoinHomegameConfirmationPageBuilder, JoinHomegameConfirmationPageBuilder>();
            RegisterComponent<IUserListPageBuilder, UserListPageBuilder>();
            RegisterComponent<IAddUserPageBuilder, AddUserPageBuilder>();
            RegisterComponent<IAddUserConfirmationPageBuilder, AddUserConfirmationPageBuilder>();
            RegisterComponent<IEditUserPageBuilder, EditUserPageBuilder>();
            RegisterComponent<IChangePasswordPageBuilder, ChangePasswordPageBuilder>();
            RegisterComponent<IForgotPasswordPageBuilder, ForgotPasswordPageBuilder>();
            RegisterComponent<ISharingIndexPageBuilder, SharingIndexPageBuilder>();
            RegisterComponent<ISharingTwitterPageBuilder, SharingTwitterPageBuilder>();
            RegisterComponent<ICashgameListTableModelFactory, CashgameListTableModelFactory>();
            RegisterComponent<ICashgameListTableItemModelFactory, CashgameListTableItemModelFactory>();
            RegisterComponent<IEditCheckpointPageBuilder, EditCheckpointPageBuilder>();

            // Model Factories
            RegisterComponent<IAvatarModelFactory, AvatarModelFactory>();
            RegisterComponent<IGoogleAnalyticsModelFactory, GoogleAnalyticsModelFactory>();
            RegisterComponent<IUserNavigationModelFactory, UserNavigationModelFactory>();
            RegisterComponent<IAdminNavigationModelFactory, AdminNavigationModelFactory>();
            RegisterComponent<ICashgameMatrixTableModelFactory, CashgameMatrixTableModelFactory>();
            RegisterComponent<ICashgameMatrixTableColumnHeaderModelFactory, CashgameMatrixTableColumnHeaderModelFactory>();
            RegisterComponent<IRunningCashgameTableModelFactory, RunningCashgameTableModelFactory>();
            RegisterComponent<IRunningCashgameTableItemModelFactory, RunningCashgameTableItemModelFactory>();
            RegisterComponent<IBarModelFactory, BarModelFactory>();
            RegisterComponent<ICashgameMatrixTableRowModelFactory, CashgameMatrixTableRowModelFactory>();
            RegisterComponent<ICashgameMatrixTableCellModelFactory, CashgameMatrixTableCellModelFactory>();
            RegisterComponent<ICashgameSuiteChartJsonBuilder, CashgameSuiteChartJsonBuilder>();
            RegisterComponent<IActionChartJsonBuilder, ActionChartJsonBuilder>();
            RegisterComponent<ICashgameDetailsChartJsonBuilder, CashgameDetailsChartJsonBuilder>();
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
            RegisterComponent<IAdminCommandProvider, AdminCommandProvider>();

            // Security
            RegisterComponent<IAuth, Auth>();
        }
    }
}