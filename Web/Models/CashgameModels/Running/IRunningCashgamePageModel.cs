using Web.Models.MiscModels;

namespace Web.Models.CashgameModels.Running
{
    public interface IRunningCashgamePageModel
    {
        string Slug { get; }
        string ApiHost { get; }
        SpinnerModel SpinnerModel { get; }
    }
}