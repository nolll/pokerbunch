using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Toplist
{
    public class CashgameToplistPageModel : CashgameContextPageModel
    {
	    public ToplistTableModel TableModel { get; private set; }

        public CashgameToplistPageModel(
            PageProperties pageProperties, 
            CashgamePageNavigationModel pageNavModel, 
            CashgameYearNavigationModel yearNavModel, 
            ToplistTableModel tableModel)
            : base(
            "Cashgame Toplist",
            pageProperties,
            pageNavModel,
            yearNavModel)
        {
            TableModel = tableModel;
        }
    }
}