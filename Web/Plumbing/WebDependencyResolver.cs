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
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.ModelFactories.CashgameModelFactories.Checkpoints;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.ModelFactories.CashgameModelFactories.List;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.ModelFactories.HomegameModelFactories;
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
            RegisterComponent<IMatrixPageBuilder, MatrixPageBuilder>();
            RegisterComponent<IAddHomegamePageBuilder, AddHomegamePageBuilder>();
            RegisterComponent<IHomegameDetailsPageBuilder, HomegameDetailsPageBuilder>();
            RegisterComponent<IEditHomegamePageBuilder, EditHomegamePageBuilder>();
            RegisterComponent<IEditCashgamePageBuilder, EditCashgamePageBuilder>();
            RegisterComponent<ICashgameChartPageBuilder, CashgameChartPageBuilder>();
            RegisterComponent<ICashgameListPageBuilder, CashgameListPageBuilder>();
            RegisterComponent<IRunningCashgamePageBuilder, RunningCashgamePageBuilder>();
            RegisterComponent<IJoinHomegamePageBuilder, JoinHomegamePageBuilder>();
            RegisterComponent<IJoinHomegameConfirmationPageBuilder, JoinHomegameConfirmationPageBuilder>();
            RegisterComponent<IEditUserPageBuilder, EditUserPageBuilder>();
            RegisterComponent<IForgotPasswordPageBuilder, ForgotPasswordPageBuilder>();
            RegisterComponent<ISharingIndexPageBuilder, SharingIndexPageBuilder>();
            RegisterComponent<ISharingTwitterPageBuilder, SharingTwitterPageBuilder>();
            RegisterComponent<ICashgameListTableModelFactory, CashgameListTableModelFactory>();
            RegisterComponent<ICashgameListTableItemModelFactory, CashgameListTableItemModelFactory>();
            RegisterComponent<IEditCheckpointPageBuilder, EditCheckpointPageBuilder>();

            // Model Factories
            RegisterComponent<IRunningCashgameTableModelFactory, RunningCashgameTableModelFactory>();
            RegisterComponent<IRunningCashgameTableItemModelFactory, RunningCashgameTableItemModelFactory>();
            RegisterComponent<ICashgameSuiteChartJsonBuilder, CashgameSuiteChartJsonBuilder>();
            RegisterComponent<IActionChartJsonBuilder, ActionChartJsonBuilder>();
            RegisterComponent<ICashgameDetailsChartJsonBuilder, CashgameDetailsChartJsonBuilder>();

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