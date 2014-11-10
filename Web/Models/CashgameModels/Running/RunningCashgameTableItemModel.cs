using Core.Services;
using Core.UseCases.RunningCashgame;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgameTableItemModel
    {
        public string Name { get; private set; }
        public string PlayerUrl { get; private set; }
        public string Buyin { get; private set; }
        public string Stack { get; private set; }
        public string Winnings { get; private set; }
        public string Time { get; private set; }
        public string WinningsClass { get; private set; }
        public bool ManagerButtonsEnabled { get; private set; }
        public string BuyinUrl { get; private set; }
        public string ReportUrl { get; private set; }
        public string CashoutUrl { get; private set; }
        public bool HasCashedOut { get; private set; }

        public RunningCashgameTableItemModel(RunningCashgameTableItem item)
        {
            Name = item.Name;
            PlayerUrl = item.PlayerUrl.Relative;
            BuyinUrl = item.BuyinUrl.Relative;
            ReportUrl = item.ReportUrl.Relative;
            CashoutUrl = item.CashoutUrl.Relative;
            Buyin = item.Buyin.String;
            Stack = item.Stack.String;
            Winnings = item.Winnings.String;
            Time = item.Time.RelativeString;
            WinningsClass = ResultFormatter.GetWinningsCssClass(item.Winnings);
            HasCashedOut = item.HasCashedOut;
            ManagerButtonsEnabled = item.CanManage;
        }
    }
}