using Application.Urls;
using Core.Entities;

namespace Application.UseCases.CashgameDetails
{
    public class PlayerResultItem
    {
        public string Name { get; private set; }
        public Url PlayerUrl { get; private set; }
        public Money Buyin { get; private set; }
        public Money Cashout { get; private set; }
        public MoneyResult Winnings { get; private set; }
        public MoneyWinRate WinRate { get; private set; }

        public PlayerResultItem(Bunch bunch, Cashgame cashgame, Player player, CashgameResult result)
        {
            Name = player.DisplayName;
            PlayerUrl = new CashgameActionUrl(bunch.Slug, cashgame.DateString, player.Id);
            Buyin = new Money(result.Buyin, bunch.Currency);
            Cashout = new Money(result.Stack, bunch.Currency);
            Winnings = new MoneyResult(result.Winnings, bunch.Currency);
            WinRate = new MoneyWinRate(result.WinRate, bunch.Currency);
        }
    }
}