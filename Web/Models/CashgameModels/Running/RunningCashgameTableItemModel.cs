using Core.Services;
using Core.Urls;
using Core.UseCases.RunningCashgame;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgameTableItemModel
    {
        public string Name { get; private set; }
        public Url PlayerUrl { get; private set; }
        public string Buyin { get; private set; }
        public string Stack { get; private set; }
        public string Winnings { get; private set; }
        public string Time { get; private set; }
        public string WinningsClass { get; private set; }
        public bool ManagerButtonsEnabled { get; private set; }
        public Url BuyinUrl { get; private set; }
        public Url ReportUrl { get; private set; }
        public Url CashoutUrl { get; private set; }
        public bool HasCashedOut { get; private set; }

        public RunningCashgameTableItemModel(RunningCashgameTableItem item)
        {
            Name = item.Name;
            PlayerUrl = item.PlayerUrl;
            BuyinUrl = item.BuyinUrl;
            ReportUrl = item.ReportUrl;
            CashoutUrl = item.CashoutUrl;
            Buyin = item.Buyin.ToString();
            Stack = item.Stack.ToString();
            Winnings = item.Winnings.ToString();
            Time = item.Time.ToString();
            WinningsClass = ResultFormatter.GetWinningsCssClass(item.Winnings);
            HasCashedOut = item.HasCashedOut;
        }
    }
}