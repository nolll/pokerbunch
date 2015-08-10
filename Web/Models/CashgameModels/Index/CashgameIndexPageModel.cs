using Core.UseCases;
using Core.UseCases.CashgameCurrentRankings;
using Web.Models.CashgameModels.CurrentRankings;
using Web.Models.CashgameModels.Status;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Index
{
    public class CashgameIndexPageModel : CashgamePageModel
    {
        public CashgameStatusModel StatusModel { get; private set; }
        public CurrentRankingsTableModel CurrentRankingsModel { get; private set; }

        public CashgameIndexPageModel(CashgameContext.Result contextResult, CashgameStatus.Result statusResult, Core.UseCases.CashgameCurrentRankings.CurrentRankings.Result currentRankingsResult)
            : base(
                "Cashgames",
                contextResult)
        {
            StatusModel = new CashgameStatusModel(statusResult);
            CurrentRankingsModel = new CurrentRankingsTableModel(currentRankingsResult);
        }
    }
}