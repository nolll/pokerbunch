using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Running{

	public class RunningCashgameTableItemModel{

	    public string Name { get; set; }
	    public string PlayerUrl { get; set; }
		public string Buyin { get; set; }
		public string Stack { get; set; }
		public string Winnings { get; set; }
		public string Time { get; set; }
		public string WinningsClass { get; set; }
		public bool ManagerButtonsEnabled { get; set; }
		public string BuyinUrl { get; set; }
		public UrlModel ReportUrl { get; set; }
		public string CashoutUrl { get; set; }
		public bool HasCashedOut { get; set; }
	}
}