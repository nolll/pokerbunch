using Core.UseCases;
using Core.UseCases.CashgameCurrentRankings;
using Core.UseCases.CashgameStatus;
using Web.Models.CashgameModels.CurrentRankings;
using Web.Models.CashgameModels.Status;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Index
{
    public class CashgameIndexPageModel : CashgamePageModel
    {
        public CashgameStatusModel StatusModel { get; private set; }
        public CurrentRankingsTableModel CurrentRankingsModel { get; private set; }

        public CashgameIndexPageModel(CashgameContext.Result contextResult, CashgameStatusResult statusResult, CurrentRankingsResult currentRankingsResult)
            : base(
                "Cashgames",
                contextResult)
        {
            StatusModel = new CashgameStatusModel(statusResult);
            CurrentRankingsModel = new CurrentRankingsTableModel(currentRankingsResult);
        }
    }
}