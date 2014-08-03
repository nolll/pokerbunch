using Core.Entities;

namespace Application.UseCases.CashgameTopList
{
    public class TopListItem
    {
        public int Rank { get; private set; }
        public int PlayerId { get; private set; }
        public string Name { get; private set; }
        public Money Winnings { get; private set; }
        public Money Buyin { get; private set; }
        public Money Cashout { get; private set; }
        public Time TimePlayed { get; private set; }
        public int GamesPlayed { get; private set; }
        public Money WinRate { get; private set; }

        public TopListItem(CashgameTotalResult totalResult, int index, Currency currency)
        {
            Buyin = new Money(totalResult.Buyin, currency);
            Cashout = new Money(totalResult.Cashout, currency);
            GamesPlayed = totalResult.GameCount;
            TimePlayed = Time.FromMinutes(totalResult.TimePlayed);
            Name = totalResult.Player.DisplayName;
            PlayerId = totalResult.Player.Id;
            Rank = index + 1;
            Winnings = new MoneyResult(totalResult.Winnings, currency);
            WinRate = new MoneyWinRate(totalResult.WinRate, currency);
        }
    }
}