using Castle.Core;
using Castle.Windsor;
using Plumbing;
using Web.Commands.CashgameCommands;
using Web.Commands.HomegameCommands;
using Web.Commands.UserCommands;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.ModelFactories.CashgameModelFactories.Checkpoints;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.ModelFactories.CashgameModelFactories.List;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.CashgameModelFactories.Running;

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
            // Page Model Factories
            RegisterComponent<IMatrixPageBuilder, MatrixPageBuilder>();
            RegisterComponent<IEditCashgamePageBuilder, EditCashgamePageBuilder>();
            RegisterComponent<ICashgameChartPageBuilder, CashgameChartPageBuilder>();
            RegisterComponent<ICashgameListPageBuilder, CashgameListPageBuilder>();
            RegisterComponent<IRunningCashgamePageBuilder, RunningCashgamePageBuilder>();
            RegisterComponent<IEditCheckpointPageBuilder, EditCheckpointPageBuilder>();

            // Model Factories
            RegisterComponent<IRunningCashgameTableModelFactory, RunningCashgameTableModelFactory>();
            RegisterComponent<IRunningCashgameTableItemModelFactory, RunningCashgameTableItemModelFactory>();
            RegisterComponent<ICashgameSuiteChartJsonBuilder, CashgameSuiteChartJsonBuilder>();
            RegisterComponent<IActionChartJsonBuilder, ActionChartJsonBuilder>();
            RegisterComponent<ICashgameDetailsChartJsonBuilder, CashgameDetailsChartJsonBuilder>();

            // Command Providers
            RegisterComponent<IUserCommandProvider, UserCommandProvider>();
            RegisterComponent<IBunchCommandProvider, BunchCommandProvider>();
            RegisterComponent<ICashgameCommandProvider, CashgameCommandProvider>();
        }
    }
}