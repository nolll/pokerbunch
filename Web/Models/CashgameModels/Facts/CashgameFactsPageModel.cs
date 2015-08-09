using Core.UseCases;
using Core.UseCases.CashgameFacts;
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
            CashgameFactsResult factsResult) :
            base(
            "Cashgame Facts",
            cashgameContextResult)
	    {
	        GameCount = factsResult.GameCount;
            TotalGameTime = factsResult.TotalTimePlayed.String;
            TotalTurnover = factsResult.Turnover.String;

            BestResultName = factsResult.BestResult.PlayerName;
            BestResultAmount = factsResult.BestResult.Amount.String;
            
            WorstResultName = factsResult.WorstResult.PlayerName;
            WorstResultAmount = factsResult.WorstResult.Amount.String;
            
            BestTotalWinningsName = factsResult.BestTotalResult.PlayerName;
            BestTotalWinningsAmount = factsResult.BestTotalResult.Amount.String;
            
            WorstTotalWinningsName = factsResult.WorstTotalResult.PlayerName;
            WorstTotalWinningsAmount = factsResult.WorstTotalResult.Amount.String;
            
            MostTimeName = factsResult.MostTimePlayed.PlayerName;
            MostTimeDuration = factsResult.MostTimePlayed.Time.String;
            
            BiggestTotalBuyinName = factsResult.BiggestBuyin.PlayerName;
            BiggestTotalBuyinAmount = factsResult.BiggestBuyin.Amount.String;
            
            BiggestTotalCashoutName = factsResult.BiggestCashout.PlayerName;
            BiggestTotalCashoutAmount = factsResult.BiggestCashout.Amount.String;
        }
	}
}