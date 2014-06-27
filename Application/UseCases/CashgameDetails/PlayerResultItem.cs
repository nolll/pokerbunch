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

        public PlayerResultItem(Homegame homegame, Cashgame cashgame, Player player, CashgameResult result)
        {
            Name = player.DisplayName;
            PlayerUrl = new CashgameActionUrl(homegame.Slug, cashgame.DateString, player.Id);
            Buyin = new Money(result.Buyin, homegame.Currency);
            Cashout = new Money(result.Stack, homegame.Currency);
            Winnings = new MoneyResult(result.Winnings, homegame.Currency);
            WinRate = new MoneyWinRate(result.WinRate, homegame.Currency);
        }
    }
}