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
        public static CashgameMatrixTableRowModel Create(Homegame homegame, CashgameSuite suite, Player player, CashgameTotalResult result, int rank)
        {
            var cellModels = CashgameMatrixTableCellModelFactory.CreateList(suite.Cashgames, player);
            
            return new CashgameMatrixTableRowModel
                {
                    Rank = rank,
                    Name = player.DisplayName,
                    UrlEncodedName = HttpUtility.UrlPathEncode(player.DisplayName),
                    PlayerUrl = new PlayerDetailsUrl(homegame.Slug, player.Id),
                    CellModels = cellModels,
                    TotalResult = Globalization.FormatResult(homegame.Currency, result.Winnings),
                    ResultClass = ResultFormatter.GetWinningsCssClass(result.Winnings)
                };
        }

        public static List<CashgameMatrixTableRowModel> CreateList(Homegame homegame, CashgameSuite suite, IEnumerable<CashgameTotalResult> results)
        {
            var models = new List<CashgameMatrixTableRowModel>();
            var rank = 0;
            foreach (var result in results)
            {
                rank++;
                models.Add(Create(homegame, suite, result.Player, result, rank));
            }
            return models;
        }
    }
}