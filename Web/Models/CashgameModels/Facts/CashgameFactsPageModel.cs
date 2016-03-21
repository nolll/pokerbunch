using Core.Services;
using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Facts
{
	public class CashgameFactsPageModel : CashgamePageModel
    {
        public int GameCount { get; private set; }
        public string TotalGameTime { get; private set; }
        public string TotalTurnover { get; private set; }
        public string BestResultAmount { get; private set; }
        public string BestResultName { get; private set; }
        public string WorstResultAmount { get; private set; }
        public string WorstResultName { get; private set; }
        public string MostTimeDuration { get; private set; }
        public string MostTimeName { get; private set; }
        public string BestTotalWinningsName { get; private set; }
        public string BestTotalWinningsAmount { get; private set; }
        public string WorstTotalWinningsName { get; private set; }
        public string WorstTotalWinningsAmount { get; private set; }
        public string BiggestTotalBuyinName { get; private set; }
        public string BiggestTotalBuyinAmount { get; private set; }
        public string BiggestTotalCashoutName { get; private set; }
        public string BiggestTotalCashoutAmount { get; private set; }

	    public CashgameFactsPageModel(
            CashgameContext.Result cashgameContextResult,
            CashgameFacts.Result factsResult) :
            base(
            "Cashgame Facts",
            cashgameContextResult)
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
            BiggestTotalBuyinAmount = ResultFormatter.FormatWinnings(factsResult.BiggestBuyin.Amount);
            
            BiggestTotalCashoutName = factsResult.BiggestCashout.PlayerName;
            BiggestTotalCashoutAmount = ResultFormatter.FormatWinnings(factsResult.BiggestCashout.Amount);
        }
	}
}