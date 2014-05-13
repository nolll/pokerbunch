using System.Collections.Generic;
using System.Linq;
using Application.UseCases.CashgameTopList;

namespace Web.Models.CashgameModels.Toplist
{
	public class ToplistTableModel
    {
	    public TopListTableColumns ColumnsModel { get; private set; }
        public IList<CashgameToplistTableItemModel> ItemModels { get; private set; }

        public ToplistTableModel(TopListResult topListResult)
	    {
            ColumnsModel = new TopListTableColumns(topListResult);
            ItemModels = topListResult.Items.Select(o => new CashgameToplistTableItemModel(o, topListResult.Slug, topListResult.OrderBy)).ToList();
	    }
    }
}