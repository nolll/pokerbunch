using Web.Models.UrlModels;
using Web.Services;

namespace Web.Models.CashgameModels.Running{

	public class RunningCashgameTableItemModel{

	    public string Name { get; set; }
	    public UrlModel PlayerUrl { get; set; }
		public string Buyin { get; set; }
		public string Stack { get; set; }
		public string Winnings { get; set; }
		public string Time { get; set; }
		public string WinningsClass { get; set; }
		public bool ManagerButtonsEnabled { get; set; }
		public UrlModel BuyinUrl { get; set; }
		public UrlModel ReportUrl { get; set; }
		public UrlModel CashoutUrl { get; set; }
		public bool HasCashedOut { get; set; }
	}
}