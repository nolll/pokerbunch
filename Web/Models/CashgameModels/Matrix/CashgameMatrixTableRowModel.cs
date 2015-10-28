using System.Collections.Generic;
using Core.Services;
using Web.Common.Urls.SiteUrls;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixTableRowModel
    {
	    public int Rank { get; private set; }
	    public string Name { get; private set; }
        public string Color { get; private set; }
        public string TotalResult { get; private set; }
	    public string ResultClass { get; private set; }
	    public string PlayerUrl { get; private set; }
        public IList<CashgameMatrixTableCellModel> CellModels { get; private set; }

        public CashgameMatrixTableRowModel(IEnumerable<Core.UseCases.Matrix.GameItem> gameItems, Core.UseCases.Matrix.MatrixPlayerItem playerItem)
        {
            Rank = playerItem.Rank;
            Name = playerItem.Name;
            Color = playerItem.Color;
            PlayerUrl = new PlayerDetailsUrl(playerItem.PlayerId).Relative;
            CellModels = CreateCells(gameItems, playerItem);
            TotalResult = ResultFormatter.FormatWinnings(playerItem.TotalResult);
            ResultClass = ResultFormatter.GetWinningsCssClass(playerItem.TotalResult);
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
                        ResultClass = ResultFormatter.GetWinningsCssClass(resultItem.Winnings),
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
    }
}