using System.Collections.Generic;
using System.Web;
using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class CashgameMatrixTableRowModelFactory : ICashgameMatrixTableRowModelFactory
    {
        private readonly IUrlProvider _urlProvider;

        public CashgameMatrixTableRowModelFactory(IUrlProvider urlProvider)
        {
            _urlProvider = urlProvider;
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
                    TotalResult = Globalization.FormatResult(homegame.Currency, winnings),
                    ResultClass = Util.GetWinningsCssClass(winnings)
                };
        }

        private List<CashgameMatrixTableCellModel> GetCellModels(IEnumerable<Cashgame> cashgames, Player player)
        {
            var models = new List<CashgameMatrixTableCellModel>();
            if (cashgames != null)
            {
                foreach (var cashgame in cashgames)
                {
                    var result = cashgame.GetResult(player);
                    models.Add(new CashgameMatrixTableCellModel(cashgame, result));
                }
            }
            return models;
        }
    }
}