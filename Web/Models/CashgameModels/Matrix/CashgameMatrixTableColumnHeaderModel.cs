using Application.Services;
using Application.Urls;
using Application.UseCases.Matrix;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixTableColumnHeaderModel
    {
        public string Date { get; private set; }
        public Url CashgameUrl { get; private set; }

        public CashgameMatrixTableColumnHeaderModel(GameItem gameItem, bool showYear = false)
        {
            Date = Globalization.FormatShortDate(gameItem.Date, showYear);
            CashgameUrl = gameItem.Url;
        }
    }
}