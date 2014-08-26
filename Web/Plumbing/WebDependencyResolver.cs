using Application.Services;
using Castle.Core;
using Castle.Windsor;
using Plumbing;
using Web.Commands.AdminCommands;
using Web.Commands.CashgameCommands;
using Web.Commands.HomegameCommands;
using Web.Commands.PlayerCommands;
using Web.Commands.SharingCommands;
using Web.Commands.UserCommands;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.ModelFactories.CashgameModelFactories.Cashout;
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.ModelFactories.CashgameModelFactories.Checkpoints;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.ModelFactories.CashgameModelFactories.End;
using Web.ModelFactories.CashgameModelFactories.List;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.CashgameModelFactories.Report;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.ModelFactories.ChartModelFactories;
using Web.ModelFactories.HomeModelFactories;
using Web.ModelFactories.HomegameModelFactories;
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

            // Page Model Factories
            RegisterComponent<IHomePageBuilder, HomePageBuilder>();
            RegisterComponent<IMatrixPageBuilder, MatrixPageBuilder>();
            RegisterComponent<IAddHomegamePageBuilder, AddHomegamePageBuilder>();
            RegisterComponent<IAddHomegameConfirmationPageBuilder, AddHomegameConfirmationPageBuilder>();
            RegisterComponent<IReportPageBuilder, ReportPageBuilder>();
            RegisterComponent<ICashoutPageBuilder, CashoutPageBuilder>();
            RegisterComponent<IEndPageBuilder, EndPageBuilder>();
            RegisterComponent<IHomegameDetailsPageBuilder, HomegameDetailsPageBuilder>();
            RegisterComponent<IEditHomegamePageBuilder, EditHomegamePageBuilder>();
            RegisterComponent<IEditCashgamePageBuilder, EditCashgamePageBuilder>();
            RegisterComponent<ICashgameChartPageBuilder, CashgameChartPageBuilder>();
            RegisterComponent<ICashgameListPageBuilder, CashgameListPageBuilder>();
            RegisterComponent<IRunningCashgamePageBuilder, RunningCashgamePageBuilder>();
            RegisterComponent<IAddPlayerPageBuilder, AddPlayerPageBuilder>();
            RegisterComponent<IAddPlayerConfirmationPageBuilder, AddPlayerConfirmationPageBuilder>();
            RegisterComponent<IInvitePlayerPageBuilder, InvitePlayerPageBuilder>();
            RegisterComponent<IInvitePlayerConfirmationPageBuilder, InvitePlayerConfirmationPageBuilder>();
            RegisterComponent<IJoinHomegamePageBuilder, JoinHomegamePageBuilder>();
            RegisterComponent<IJoinHomegameConfirmationPageBuilder, JoinHomegameConfirmationPageBuilder>();
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
            RegisterComponent<ICashgameMatrixTableModelFactory, CashgameMatrixTableModelFactory>();
            RegisterComponent<ICashgameMatrixTableColumnHeaderModelFactory, CashgameMatrixTableColumnHeaderModelFactory>();
            RegisterComponent<IRunningCashgameTableModelFactory, RunningCashgameTableModelFactory>();
            RegisterComponent<IRunningCashgameTableItemModelFactory, RunningCashgameTableItemModelFactory>();
            RegisterComponent<ICashgameMatrixTableRowModelFactory, CashgameMatrixTableRowModelFactory>();
            RegisterComponent<ICashgameMatrixTableCellModelFactory, CashgameMatrixTableCellModelFactory>();
            RegisterComponent<ICashgameSuiteChartJsonBuilder, CashgameSuiteChartJsonBuilder>();
            RegisterComponent<IActionChartJsonBuilder, ActionChartJsonBuilder>();
            RegisterComponent<ICashgameDetailsChartJsonBuilder, CashgameDetailsChartJsonBuilder>();
            RegisterComponent<IChartValueModelFactory, ChartValueModelFactory>();

            // Mappers
            RegisterComponent<ICheckpointModelMapper, CheckpointModelMapper>();

            // Command Providers
            RegisterComponent<IPlayerCommandProvider, PlayerCommandProvider>();
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