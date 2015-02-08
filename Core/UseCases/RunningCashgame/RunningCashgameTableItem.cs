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
        public bool HasCashedOut { get; private set; }

        public RunningCashgameTableItem(string name, Url playerUrl, Money buyin, Money stack, Money winnings, Time time, bool hasCashedOut)
        {
            Name = name;
            PlayerUrl = playerUrl;
            Buyin = buyin;
            Stack = stack;
            Winnings = winnings;
            Time = time;
            HasCashedOut = hasCashedOut;
        }
    }
}