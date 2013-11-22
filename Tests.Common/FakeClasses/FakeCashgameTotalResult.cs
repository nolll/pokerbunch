using Core.Classes;

namespace Tests.Common.FakeClasses
{
    public class FakeCashgameTotalResult : CashgameTotalResult
    {
        public FakeCashgameTotalResult(
            int winnings = default(int),
            int gameCount = default(int),
            int timePlayed = default(int),
            int winRate = default(int),
            Player player = default(Player))
            : base(
                winnings, 
                gameCount, 
                timePlayed, 
                winRate, 
                player)
        {
        }
    }
}