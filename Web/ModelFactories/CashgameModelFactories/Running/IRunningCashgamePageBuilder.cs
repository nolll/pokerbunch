using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public interface IRunningCashgamePageBuilder
    {
        RunningCashgamePageModel Build(string slug);
    }
}