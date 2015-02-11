using Core.UseCases.BunchContext;
using Core.UseCases.CashgameTopList;
using Web.Models.CashgameModels.Toplist;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Index
{
    public class CashgameIndexPageModel : BunchPageModel
    {
        public string TopListHeading { get; private set; }
        public ToplistTableModel TopListModel { get; private set; }

        public CashgameIndexPageModel(
            BunchContextResult bunchContextResult,
            TopListResult topListResult)
            : base(
                "Cashgames",
                bunchContextResult)
        {
            TopListHeading = string.Format("Cashgame Rankings {0}", topListResult.Year);
            TopListModel = new ToplistTableModel(topListResult);
        }
    }
}