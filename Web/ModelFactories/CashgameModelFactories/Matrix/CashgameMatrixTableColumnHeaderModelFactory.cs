using Application.Services;
using Core.Entities;
using Web.Models.CashgameModels.Matrix;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class CashgameMatrixTableColumnHeaderModelFactory : ICashgameMatrixTableColumnHeaderModelFactory
    {
        private readonly IGlobalization _globalization;

        public CashgameMatrixTableColumnHeaderModelFactory(
            IGlobalization globalization)
        {
            _globalization = globalization;
        }

        public CashgameMatrixTableColumnHeaderModel Create(Homegame homegame, Cashgame cashgame, bool showYear = false)
        {
            return new CashgameMatrixTableColumnHeaderModel
                {
                    Date = cashgame.StartTime.HasValue ? _globalization.FormatShortDate(cashgame.StartTime.Value, showYear) : string.Empty,
                    CashgameUrl = new CashgameDetailsUrlModel(homegame.Slug, cashgame.DateString)
                };
        }
    }
}