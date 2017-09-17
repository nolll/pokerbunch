using Core.UseCases;
using Web.Extensions;
using Web.Models.CashgameModels.CurrentRankings;
using Web.Models.CashgameModels.Status;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Index
{
    public class CashgameIndexPageModel : CashgamePageModel
    {
        public CashgameStatusModel StatusModel { get; }
        public CurrentRankingsTableModel CurrentRankingsModel { get; }
        public bool HasGames { get; }

        public CashgameIndexPageModel(CashgameContext.Result contextResult, CashgameStatus.Result statusResult, Core.UseCases.CurrentRankings.Result currentRankingsResult)
            : base(contextResult)
        {
            StatusModel = new CashgameStatusModel(statusResult);
            CurrentRankingsModel = new CurrentRankingsTableModel(currentRankingsResult);
            HasGames = currentRankingsResult.HasGames;
        }

        public override string BrowserTitle => "Cashgames";

        public override View GetView()
        {
            return new View("~/Views/Pages/CashgameIndex/CashgameIndex.cshtml");
        }
    }
}