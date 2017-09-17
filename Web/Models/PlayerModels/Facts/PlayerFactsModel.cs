using System;
using Core.Services;
using Core.UseCases;
using Web.Extensions;
using Web.Services;

namespace Web.Models.PlayerModels.Facts
{
	public class PlayerFactsModel : IViewModel
    {
        public string Winnings { get; }
        public string WinningsCssClass { get; }
        public string BestResult { get; }
        public string BestResultCssClass { get; }
        public string WorstResult { get; }
        public string WorstResultCssClass { get; }
        public int GamesPlayed { get; }
	    public string TimePlayed { get; }
        public int BestResultCount { get; }
        public string CurrentStreak { get; }
        public string WinningStreak { get; }
        public string LosingStreak { get; }

	    public PlayerFactsModel(PlayerFacts.Result factsResult)
	    {
	        Winnings = ResultFormatter.FormatWinnings(factsResult.Winnings);
	        WinningsCssClass = CssService.GetWinningsCssClass(factsResult.Winnings);
	        BestResult = ResultFormatter.FormatWinnings(factsResult.BestResult);
            BestResultCssClass = CssService.GetWinningsCssClass(factsResult.BestResult); 
            WorstResult = ResultFormatter.FormatWinnings(factsResult.WorstResult);
            WorstResultCssClass = CssService.GetWinningsCssClass(factsResult.WorstResult);
            GamesPlayed = factsResult.GamesPlayed;
	        TimePlayed = factsResult.TimePlayed.ToString();
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

	    public View GetView()
	    {
	        return new View("PlayerDetails/PlayerFacts");
	    }
    }
}