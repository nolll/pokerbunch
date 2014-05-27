using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.List
{
    public class CashgameListPageModel : IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public CashgameListTableModel ListTableModel { get; set; }
        public CashgamePageNavigationModel PageNavModel { get; set; }
        public CashgameYearNavigationModel YearNavModel { get; set; }
    }
}