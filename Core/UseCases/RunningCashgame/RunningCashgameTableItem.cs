using Core.Entities;
using Core.Urls;

namespace Core.UseCases.RunningCashgame
{
    public class RunningCashgameTableItem
    {
        public string Name { get; private set; }
        public Url PlayerUrl { get; private set; }
        public Money Buyin { get; private set; }
        public Money Stack { get; private set; }
        public Money Winnings { get; private set; }
        public Time Time { get; private set; }
        public Url BuyinUrl { get; private set; }
        public Url ReportUrl { get; private set; }
        public Url CashoutUrl { get; private set; }
        public bool HasCashedOut { get; private set; }
        public bool CanManage { get; private set; }

        public RunningCashgameTableItem(string name, Url playerUrl, Money buyin, Money stack, Money winnings, Time time, Url buyinUrl, Url reportUrl, Url cashoutUrl, bool hasCashedOut, bool canManage)
        {
            Name = name;
            PlayerUrl = playerUrl;
            Buyin = buyin;
            Stack = stack;
            Winnings = winnings;
            Time = time;
            BuyinUrl = buyinUrl;
            ReportUrl = reportUrl;
            CashoutUrl = cashoutUrl;
            HasCashedOut = hasCashedOut;
            CanManage = canManage;
        }
    }
}