namespace Core.UseCases.CashgameTopList
{
    public class TopListItem
    {
        public string Name { get; set; }
        public int Winnings { get; set; }
        public int Buyin { get; set; }
        public int Cashout { get; set; }
        public int MinutesPlayed { get; set; }
        public int GamesPlayed { get; set; }
        public int WinRate { get; set; }
    }
}