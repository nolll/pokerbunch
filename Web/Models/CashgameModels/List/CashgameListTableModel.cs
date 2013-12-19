using System.Collections.Generic;

namespace Web.Models.CashgameModels.List{

	public class CashgameListTableModel{

		public bool ShowYear { get; set; }
		public List<CashgameListTableItemModel> ListItemModels { get; set; }
	    public object DateSortClass { get; set; }
	    public object DateSortUrl { get; set; }
	    public object PlayerSortClass { get; set; }
	    public object PlayerSortUrl { get; set; }
	    public object LocationSortClass { get; set; }
	    public object LocationSortUrl { get; set; }
	    public object DurationSortClass { get; set; }
	    public object DurationSortUrl { get; set; }
	    public object TurnoverSortClass { get; set; }
	    public object TurnoverSortUrl { get; set; }
	    public object AverageBuyinSortClass { get; set; }
	    public object AverageBuyinSortUrl { get; set; }
	}
}