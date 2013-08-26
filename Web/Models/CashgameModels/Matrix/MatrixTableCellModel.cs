using Core.Classes;

namespace Web.Models.CashgameModels.Matrix{
    public class MatrixTableCellModel{

	    public int Buyin { get; set; }
	    public int Cashout { get; set; }
	    public string Winnings { get; set; }
	    public bool ShowResult { get; set; }
	    public string ResultClass { get; set; }
	    public bool ShowTransactions { get; set; }
	    public bool HasBestResult { get; set; }
	    public string WinnerClass { get; set; }

		public MatrixTableCellModel(Cashgame cashgame, CashgameResult result){
			if(result != null){
				ShowResult = true;
				ShowTransactions = result.Buyin > 0;
				Buyin = result.Buyin;
				Cashout = result.Stack;
				Winnings = Util.FormatWinnings(result.Winnings);
				ResultClass = Util.GetWinningsCssClass(result.Winnings);
				HasBestResult = cashgame.IsBestResult(result);
			    WinnerClass = HasBestResult ? "winner" : null;
			} else {
				ShowResult = false;
			}
		}

	}

}