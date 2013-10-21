using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Leaderboard{

	public class CashgameLeaderboardTableItemModel{

	    public int Rank { get; set; }
	    public string Name { get; set; }
	    public string UrlEncodedName { get; set; }
	    public string TotalResult { get; set; }
	    public string ResultClass { get; set; }
	    public string GameTime { get; set; }
	    public string WinRate { get; set; }
	    public string PlayerUrl { get; set; }

	}
}