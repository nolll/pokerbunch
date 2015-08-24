using Core.Services;
using Web.Urls;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixTableColumnHeaderModel
    {
        public string Date { get; private set; }
        public string CashgameUrl { get; private set; }

        public CashgameMatrixTableColumnHeaderModel(Core.UseCases.Matrix.GameItem gameItem, bool showYear = false)
        {
            Date = Globalization.FormatShortDate(gameItem.Date, showYear);
            CashgameUrl = new CashgameDetailsUrl(gameItem.Id).Relative;
        }
    }
}