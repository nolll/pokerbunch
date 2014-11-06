using Core.UseCases.PlayerFacts;

namespace Web.Models.PlayerModels.Facts
{
	public class PlayerFactsModel
    {
        public string Winnings { get; private set; }
	    public string BestResult { get; private set; }
	    public string WorstResult { get; private set; }
	    public int GamesPlayed { get; private set; }
	    public string TimePlayed { get; private set; }
        public int BestResultCount { get; private set; }
        public int WinningStreak { get; private set; }
        public int LosingStreak { get; private set; }

	    public PlayerFactsModel(PlayerFactsResult factsResult)
	    {
	        Winnings = factsResult.Winnings.String;
	        BestResult = factsResult.BestResult.String;
	        WorstResult = factsResult.WorstResult.String;
	        GamesPlayed = factsResult.GamesPlayed;
	        TimePlayed = factsResult.TimePlayed.String;
	        BestResultCount = factsResult.BestResultCount;
	        WinningStreak = factsResult.WinningStreak;
	        LosingStreak = factsResult.LosingStreak;
	    }
    }
}