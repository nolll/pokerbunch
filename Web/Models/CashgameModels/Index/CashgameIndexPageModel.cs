using Core.UseCases.CashgameContext;
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

        public CashgameIndexPageModel(CashgameContextResult contextResult, CashgameStatusResult statusResult, CurrentRankingsResult currentRankingsResult)
            : base(
                "Cashgames",
                contextResult)
        {
            StatusModel = new CashgameStatusModel(statusResult);
            CurrentRankingsModel = new CurrentRankingsTableModel(currentRankingsResult);
        }
    }
}