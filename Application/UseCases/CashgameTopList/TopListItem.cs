using Core.Entities;

namespace Application.UseCases.CashgameTopList
{
    public class TopListItem
    {
        public int Rank { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public Money Winnings { get; set; }
        public Money Buyin { get; set; }
        public Money Cashout { get; set; }
        public Time TimePlayed { get; set; }
        public int GamesPlayed { get; set; }
        public Money WinRate { get; set; }
    }
}