using System.Collections.Generic;
using Web.Models.ChartModels;

namespace Web.Models.CashgameModels.Details
{
    public class DetailsChartModel : ChartModel
    {
        public DetailsChartModel(IList<ChartColumnModel> columns, IList<ChartRowModel> rows)
            : base(columns, rows)
        {
        }
    }
}