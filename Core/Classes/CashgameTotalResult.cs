namespace Core.Classes{

	public class CashgameTotalResult{

	    public int Winnings { get; private set; }
        public int GameCount { get; private set; }
        public int TimePlayed { get; private set; }
        public int WinRate { get; private set; }
        public Player Player { get; private set; }

	    public CashgameTotalResult(
            int winnings, 
            int gameCount, 
            int timePlayed, 
            int winRate, 
            Player player)
	    {
	        Winnings = winnings;
	        GameCount = gameCount;
	        TimePlayed = timePlayed;
	        WinRate = winRate;
	        Player = player;
	    }
	}
}