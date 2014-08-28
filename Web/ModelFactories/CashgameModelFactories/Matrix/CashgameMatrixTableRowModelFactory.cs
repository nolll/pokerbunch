using System.Collections.Generic;
using System.Web;
using Application.Services;
using Application.Urls;
using Core.Entities;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public static class CashgameMatrixTableRowModelFactory
    {
        public static CashgameMatrixTableRowModel Create(Bunch bunch, CashgameSuite suite, Player player, CashgameTotalResult result, int rank)
        {
            var cellModels = CashgameMatrixTableCellModelFactory.CreateList(suite.Cashgames, player);
            
            return new CashgameMatrixTableRowModel
                {
                    Rank = rank,
                    Name = player.DisplayName,
                    UrlEncodedName = HttpUtility.UrlPathEncode(player.DisplayName),
                    PlayerUrl = new PlayerDetailsUrl(bunch.Slug, player.Id),
                    CellModels = cellModels,
                    TotalResult = Globalization.FormatResult(bunch.Currency, result.Winnings),
                    ResultClass = ResultFormatter.GetWinningsCssClass(result.Winnings)
                };
        }

        public static List<CashgameMatrixTableRowModel> CreateList(Bunch bunch, CashgameSuite suite, IEnumerable<CashgameTotalResult> results)
        {
            var models = new List<CashgameMatrixTableRowModel>();
            var rank = 0;
            foreach (var result in results)
            {
                rank++;
                models.Add(Create(bunch, suite, result.Player, result, rank));
            }
            return models;
        }
    }
}