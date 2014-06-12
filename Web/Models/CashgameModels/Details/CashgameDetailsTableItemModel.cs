using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Details{
	
	public class CashgameDetailsTableItemModel{

	    public string Name { get; set; }
		public Url PlayerUrl { get; set; }
		public string Buyin { get; set; }
		public string Cashout { get; set; }
		public string Winnings { get; set; }
		public string WinningsClass { get; set; }
		public string Winrate { get; set; }
	}
}