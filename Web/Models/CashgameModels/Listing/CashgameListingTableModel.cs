using System.Collections.Generic;

namespace Web.Models.CashgameModels.Listing{

	public class CashgameListingTableModel{

		public bool ShowYear { get; set; }
		public List<CashgameListingTableItemModel> ListItemModels { get; set; }

	}
}