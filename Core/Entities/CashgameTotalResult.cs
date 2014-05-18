namespace Core.Entities{

	public class CashgameTotalResult
    {
        public int Winnings { get; private set; }
        public int GameCount { get; private set; }
        public int TimePlayed { get; private set; }
        public int WinRate { get; private set; }
        public int PlayerId { get; private set; }
	    public int Buyin { get; private set; }
	    public int Cashout { get; private set; }

	    public CashgameTotalResult(
            int winnings, 
            int gameCount, 
            int timePlayed, 
            int winRate, 
            int playerId,
            int buyin,
            int cashout)
	    {
	        Winnings = winnings;
	        GameCount = gameCount;
	        TimePlayed = timePlayed;
	        WinRate = winRate;
	        PlayerId = playerId;
	        Buyin = buyin;
	        Cashout = cashout;
	    }
	}
}