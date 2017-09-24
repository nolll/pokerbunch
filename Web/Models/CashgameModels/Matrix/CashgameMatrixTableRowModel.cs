using System.Collections.Generic;
using Core.Services;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Services;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixTableRowModel : IViewModel
    {
	    public int Rank { get; }
	    public string Name { get; }
        public string Color { get; }
        public string TotalResult { get; }
	    public string ResultClass { get; }
	    public string PlayerUrl { get; }
        public IList<CashgameMatrixTableCellModel> CellModels { get; }

        public CashgameMatrixTableRowModel(IEnumerable<Core.UseCases.Matrix.GameItem> gameItems, Core.UseCases.Matrix.MatrixPlayerItem playerItem)
        {
            Rank = playerItem.Rank;
            Name = playerItem.Name;
            Color = playerItem.Color;
            PlayerUrl = new PlayerDetailsUrl(playerItem.PlayerId).Relative;
            CellModels = CreateCells(gameItems, playerItem);
            TotalResult = ResultFormatter.FormatWinnings(playerItem.TotalResult);
            ResultClass = CssService.GetWinningsCssClass(playerItem.TotalResult);
        }

        private static IList<CashgameMatrixTableCellModel> CreateCells(IEnumerable<Core.UseCases.Matrix.GameItem> gameItems, Core.UseCases.Matrix.MatrixPlayerItem playerItem)
        {
            var models = new List<CashgameMatrixTableCellModel>();
            foreach (var gameItem in gameItems)
            {
                Core.UseCases.Matrix.MatrixResultItem resultItem;
                if (playerItem.ResultItems.TryGetValue(gameItem.Id, out resultItem))
                {
                    var model = new CashgameMatrixTableCellModel
                    {
                        ShowResult = true,
                        ShowTransactions = resultItem.HasTransactions,
                        Buyin = resultItem.Buyin.Amount,
                        Cashout = resultItem.Cashout.Amount,
                        Winnings = ResultFormatter.FormatWinnings(resultItem.Winnings.Amount),
                        ResultClass = CssService.GetWinningsCssClass(resultItem.Winnings),
                        WinnerClass = resultItem.HasBestResult ? "matrix__winner" : null
                    };
                    models.Add(model);
                }
                else
                {
                    models.Add(new CashgameMatrixTableCellModel());
                }
            }
            return models;
        }

        public View GetView()
        {
            return new View("Matrix/MatrixTableRow");
        }
    }
}