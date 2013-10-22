using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class CashgameMatrixTableColumnHeaderModelFactory : ICashgameMatrixTableColumnHeaderModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public CashgameMatrixTableColumnHeaderModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
        }

        public CashgameMatrixTableColumnHeaderModel Create(Homegame homegame, Cashgame cashgame, bool showYear = false)
        {
            return new CashgameMatrixTableColumnHeaderModel
                {
                    Date = cashgame.StartTime.HasValue ? StaticGlobalization.FormatShortDate(cashgame.StartTime.Value, showYear) : string.Empty,
                    CashgameUrl = _urlProvider.GetCashgameDetailsUrl(homegame, cashgame)
                };
        }
    }
}