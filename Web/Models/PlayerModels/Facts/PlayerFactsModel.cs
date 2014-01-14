namespace Web.Models.PlayerModels.Facts{

	public class PlayerFactsModel
    {
        public string Winnings { get; set; }
	    public string BestResult { get; set; }
	    public string WorstResult { get; set; }
	    public int GamesPlayed { get; set; }
	    public string TimePlayed { get; set; }
        public int BestResultCount { get; set; }
        public int WinningStreak { get; set; }
        public int LosingStreak { get; set; }
	}
}