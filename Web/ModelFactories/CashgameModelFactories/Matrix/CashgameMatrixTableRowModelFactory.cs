using System.Collections.Generic;
using System.Web;
using Application.Services;
using Application.Urls;
using Core.Entities;
using Web.Models.CashgameModels.Matrix;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class CashgameMatrixTableRowModelFactory : ICashgameMatrixTableRowModelFactory
    {
        private readonly ICashgameMatrixTableCellModelFactory _cashgameMatrixTableCellModelFactory;
        private readonly IGlobalization _globalization;

        public CashgameMatrixTableRowModelFactory(
            ICashgameMatrixTableCellModelFactory cashgameMatrixTableCellModelFactory,
            IGlobalization globalization)
        {
            _cashgameMatrixTableCellModelFactory = cashgameMatrixTableCellModelFactory;
            _globalization = globalization;
        }

        public CashgameMatrixTableRowModel Create(Homegame homegame, CashgameSuite suite, Player player, CashgameTotalResult result, int rank)
        {
            var cellModels = _cashgameMatrixTableCellModelFactory.CreateList(suite.Cashgames, player);
            
            return new CashgameMatrixTableRowModel
                {
                    Rank = rank,
                    Name = player.DisplayName,
                    UrlEncodedName = HttpUtility.UrlPathEncode(player.DisplayName),
                    PlayerUrl = new PlayerDetailsUrl(homegame.Slug, player.Id),
                    CellModels = cellModels,
                    TotalResult = _globalization.FormatResult(homegame.Currency, result.Winnings),
                    ResultClass = ResultFormatter.GetWinningsCssClass(result.Winnings)
                };
        }

        public List<CashgameMatrixTableRowModel> CreateList(Homegame homegame, CashgameSuite suite, IEnumerable<CashgameTotalResult> results)
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