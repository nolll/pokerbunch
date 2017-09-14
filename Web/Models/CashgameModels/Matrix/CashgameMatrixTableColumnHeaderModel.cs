using Core.Services;
using Web.Extensions;
using Web.Urls.SiteUrls;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixTableColumnHeaderModel : IViewModel
    {
        public string Date { get; private set; }
        public string CashgameUrl { get; private set; }

        public CashgameMatrixTableColumnHeaderModel(Core.UseCases.Matrix.GameItem gameItem, bool showYear = false)
        {
            Date = Globalization.FormatShortDate(gameItem.Date, showYear);
            CashgameUrl = new CashgameDetailsUrl(gameItem.Id).Relative;
        }

        public View GetView()
        {
            return new View("Matrix/MatrixTableColumnHeader");
        }
    }
}