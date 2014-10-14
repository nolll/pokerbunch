using System.Collections.Generic;
using Web.Models.ChartModels;

namespace Web.Models.CashgameModels.Action
{
    public class ActionChartModel : ChartModel
    {
        public ActionChartModel(IList<ChartColumnModel> columns, IList<ChartRowModel> rows)
            : base(columns, rows)
        {
        }
    }
}