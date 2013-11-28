using System.Collections.Generic;
using System.Web;
using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.Models.CashgameModels.Matrix;
using Web.Services;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class CashgameMatrixTableRowModelFactory : ICashgameMatrixTableRowModelFactory
    {
        private readonly IUrlProvider _urlProvider;
        private readonly ICashgameMatrixTableCellModelFactory _cashgameMatrixTableCellModelFactory;
        private readonly IResultFormatter _resultFormatter;
        private readonly IGlobalization _globalization;

        public CashgameMatrixTableRowModelFactory(
            IUrlProvider urlProvider,
            ICashgameMatrixTableCellModelFactory cashgameMatrixTableCellModelFactory,
            IResultFormatter resultFormatter,
            IGlobalization globalization)
        {
            _urlProvider = urlProvider;
            _cashgameMatrixTableCellModelFactory = cashgameMatrixTableCellModelFactory;
            _resultFormatter = resultFormatter;
            _globalization = globalization;
        }

        public CashgameMatrixTableRowModel Create(Homegame homegame, CashgameSuite suite, CashgameTotalResult result, int rank)
        {
            var cashgames = suite.Cashgames;
            var player = result.Player;
            var winnings = result.Winnings;
            
            return new CashgameMatrixTableRowModel
                {
                    Rank = rank,
                    Name = player.DisplayName,
                    UrlEncodedName = HttpUtility.UrlPathEncode(player.DisplayName),
                    PlayerUrl = _urlProvider.GetPlayerDetailsUrl(homegame, player),
                    CellModels = GetCellModels(cashgames, player),
                    TotalResult = _globalization.FormatResult(homegame.Currency, winnings),
                    ResultClass = _resultFormatter.GetWinningsCssClass(winnings)
                };
        }

        private List<CashgameMatrixTableCellModel> GetCellModels(IEnumerable<Cashgame> cashgames, Player player)
        {
            var models = new List<CashgameMatrixTableCellModel>();
            if (cashgames != null)
            {
                foreach (var cashgame in cashgames)
                {
                    var result = cashgame.GetResult(player.Id);
                    models.Add(_cashgameMatrixTableCellModelFactory.Create(cashgame, result));
                }
            }
            return models;
        }
    }
}