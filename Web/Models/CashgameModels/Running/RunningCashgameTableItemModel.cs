using Web.Models.UrlModels;
using Web.Services;

namespace Web.Models.CashgameModels.Running{

	public class RunningCashgameTableItemModel{

	    public string Name { get; set; }
	    public Url PlayerUrl { get; set; }
		public string Buyin { get; set; }
		public string Stack { get; set; }
		public string Winnings { get; set; }
		public string Time { get; set; }
		public string WinningsClass { get; set; }
		public bool ManagerButtonsEnabled { get; set; }
		public Url BuyinUrl { get; set; }
		public Url ReportUrl { get; set; }
		public Url CashoutUrl { get; set; }
		public bool HasCashedOut { get; set; }
	}
}