using Core.Services;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixTableColumnHeaderModel : IViewModel
    {
        public string Date { get; }
        public string CashgameUrl { get; }

        public CashgameMatrixTableColumnHeaderModel(Core.UseCases.Matrix.GameItem gameItem, bool showYear = false)
        {
            Date = Globalization.FormatShortDate(gameItem.Date, showYear);
            CashgameUrl = new CashgameDetailsUrl(gameItem.BunchId, gameItem.Id).Relative;
        }

        public View GetView()
        {
            return new View("Matrix/MatrixTableColumnHeader");
        }
    }
}