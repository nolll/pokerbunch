using Application.Services;
using Application.Urls;
using Core.Entities;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class CashgameMatrixTableColumnHeaderModelFactory : ICashgameMatrixTableColumnHeaderModelFactory
    {
        public CashgameMatrixTableColumnHeaderModel Create(Homegame homegame, Cashgame cashgame, bool showYear = false)
        {
            return new CashgameMatrixTableColumnHeaderModel
                {
                    Date = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value, showYear) : string.Empty,
                    CashgameUrl = new CashgameDetailsUrl(homegame.Slug, cashgame.DateString)
                };
        }
    }
}