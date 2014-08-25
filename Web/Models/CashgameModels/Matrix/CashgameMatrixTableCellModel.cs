namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixTableCellModel
    {
        public int Buyin { get; set; }
        public int Cashout { get; set; }
        public string Winnings { get; set; }
        public bool ShowResult { get; set; }
        public string ResultClass { get; set; }
        public bool ShowTransactions { get; set; }
        public bool HasBestResult { get; set; }
        public string WinnerClass { get; set; }
    }
}