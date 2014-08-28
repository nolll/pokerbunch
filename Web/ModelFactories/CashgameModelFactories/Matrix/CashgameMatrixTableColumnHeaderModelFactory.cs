using Application.Services;
using Application.Urls;
using Core.Entities;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public static class CashgameMatrixTableColumnHeaderModelFactory
    {
        public static CashgameMatrixTableColumnHeaderModel Create(Bunch bunch, Cashgame cashgame, bool showYear = false)
        {
            return new CashgameMatrixTableColumnHeaderModel
                {
                    Date = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value, showYear) : string.Empty,
                    CashgameUrl = new CashgameDetailsUrl(bunch.Slug, cashgame.DateString)
                };
        }
    }
}