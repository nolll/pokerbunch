using Web.Models.MiscModels;

namespace Web.Models.CashgameModels.Running
{
    public interface IRunningCashgamePageModel
    {
        string Slug { get; }
        SpinnerModel SpinnerModel { get; }
    }
}