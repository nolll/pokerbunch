using Core.Services;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Facts
{
	public class CashgameFactsPageModel : CashgamePageModel
    {
        public int GameCount { get; }
        public string TotalGameTime { get; }
        public string TotalTurnover { get; }
        public string BestResultAmount { get; }
        public string BestResultName { get; }
        public string WorstResultAmount { get; }
        public string WorstResultName { get; }
        public string MostTimeDuration { get; }
        public string MostTimeName { get; }
        public string BestTotalWinningsName { get; }
        public string BestTotalWinningsAmount { get; }
        public string WorstTotalWinningsName { get; }
        public string WorstTotalWinningsAmount { get; }
        public string BiggestTotalBuyinName { get; }
        public string BiggestTotalBuyinAmount { get; }
        public string BiggestTotalCashoutName { get; }
        public string BiggestTotalCashoutAmount { get; }

	    public CashgameFactsPageModel(CashgameContext.Result cashgameContextResult, CashgameFacts.Result factsResult)
            : base(cashgameContextResult)
	    {
	        GameCount = factsResult.GameCount;
            TotalGameTime = factsResult.TotalTimePlayed.ToString();
            TotalTurnover = factsResult.Turnover.ToString();

            BestResultName = factsResult.BestResult.PlayerName;
            BestResultAmount = ResultFormatter.FormatWinnings(factsResult.BestResult.Amount);
            
            WorstResultName = factsResult.WorstResult.PlayerName;
            WorstResultAmount = ResultFormatter.FormatWinnings(factsResult.WorstResult.Amount);
            
            BestTotalWinningsName = factsResult.BestTotalResult.PlayerName;
            BestTotalWinningsAmount = ResultFormatter.FormatWinnings(factsResult.BestTotalResult.Amount);
            
            WorstTotalWinningsName = factsResult.WorstTotalResult.PlayerName;
            WorstTotalWinningsAmount = ResultFormatter.FormatWinnings(factsResult.WorstTotalResult.Amount);
            
            MostTimeName = factsResult.MostTimePlayed.PlayerName;
            MostTimeDuration = factsResult.MostTimePlayed.Time.ToString();
            
            BiggestTotalBuyinName = factsResult.BiggestBuyin.PlayerName;
            BiggestTotalBuyinAmount = factsResult.BiggestBuyin.Amount.ToString();
            
            BiggestTotalCashoutName = factsResult.BiggestCashout.PlayerName;
            BiggestTotalCashoutAmount = factsResult.BiggestCashout.Amount.ToString();
        }

	    public override string BrowserTitle => "Cashgame Facts";

	    public override View GetView()
	    {
	        return new View("~/Views/Pages/CashgameFacts/FactsPage.cshtml");
	    }
    }
}