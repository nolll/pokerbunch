using System.Collections.Generic;

namespace Web.Models.CashgameModels.List{

	public class CashgameListTableModel{

		public bool ShowYear { get; set; }
		public List<CashgameListTableItemModel> ListItemModels { get; set; }

	}
}