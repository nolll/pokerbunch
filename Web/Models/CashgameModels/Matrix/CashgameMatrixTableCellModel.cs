using Web.Extensions;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixTableCellModel : IViewModel
    {
        public int Buyin { get; set; }
        public int Cashout { get; set; }
        public string Winnings { get; set; }
        public bool ShowResult { get; set; }
        public string ResultClass { get; set; }
        public bool ShowTransactions { get; set; }
        public string WinnerClass { get; set; }

        public View GetView()
        {
            return new View("~/Views/Pages/Matrix/MatrixTableCell.cshtml");
        }
    }
}