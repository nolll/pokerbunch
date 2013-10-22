using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class CashgameMatrixTableColumnHeaderModelFactory : ICashgameMatrixTableColumnHeaderModelFactory
    {
        private readonly IUrlProvider _urlProvider;
        private readonly IGlobalization _globalization;

        public CashgameMatrixTableColumnHeaderModelFactory(
            IUrlProvider urlProvider,
            IGlobalization globalization)
        {
            _urlProvider = urlProvider;
            _globalization = globalization;
        }

        public CashgameMatrixTableColumnHeaderModel Create(Homegame homegame, Cashgame cashgame, bool showYear = false)
        {
            return new CashgameMatrixTableColumnHeaderModel
                {
                    Date = cashgame.StartTime.HasValue ? _globalization.FormatShortDate(cashgame.StartTime.Value, showYear) : string.Empty,
                    CashgameUrl = _urlProvider.GetCashgameDetailsUrl(homegame, cashgame)
                };
        }
    }
}