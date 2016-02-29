using System;
using Core.Services;
using Core.UseCases;

namespace Web.Models.PlayerModels.Facts
{
	public class PlayerFactsModel
    {
        public string Winnings { get; private set; }
        public string WinningsCssClass { get; private set; }
        public string BestResult { get; private set; }
        public string BestResultCssClass { get; private set; }
        public string WorstResult { get; private set; }
        public string WorstResultCssClass { get; private set; }
        public int GamesPlayed { get; private set; }
	    public string TimePlayed { get; private set; }
        public int BestResultCount { get; private set; }
        public string CurrentStreak { get; private set; }
        public string WinningStreak { get; private set; }
        public string LosingStreak { get; private set; }

	    public PlayerFactsModel(PlayerFacts.Result factsResult)
	    {
	        Winnings = factsResult.Winnings.String;
	        WinningsCssClass = ResultFormatter.GetWinningsCssClass(factsResult.Winnings);
	        BestResult = factsResult.BestResult.String;
            BestResultCssClass = ResultFormatter.GetWinningsCssClass(factsResult.BestResult); 
            WorstResult = factsResult.WorstResult.String;
            WorstResultCssClass = ResultFormatter.GetWinningsCssClass(factsResult.WorstResult);
            GamesPlayed = factsResult.GamesPlayed;
	        TimePlayed = factsResult.TimePlayed.String;
	        BestResultCount = factsResult.BestResultCount;
            CurrentStreak = FormatStreak(factsResult.CurrentStreak);
            WinningStreak = FormatWinningStreak(factsResult.WinningStreak);
            LosingStreak = FormatLosingStreak(factsResult.LosingStreak);
	    }

        private string FormatStreak(int streak)
        {
            var absStreak = Math.Abs(streak);
            return streak < 0 ? FormatLosingStreak(absStreak) : FormatWinningStreak(absStreak);
        }

        private string FormatLosingStreak(int streak)
        {
            return $"Lost in {streak} games";
        }

        private string FormatWinningStreak(int streak)
        {
            return $"Won in {streak} games";
        }
    }
}