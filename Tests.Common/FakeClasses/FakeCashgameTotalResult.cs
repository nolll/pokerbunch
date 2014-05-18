using Core.Entities;

namespace Tests.Common.FakeClasses
{
    public class FakeCashgameTotalResult : CashgameTotalResult
    {
        public FakeCashgameTotalResult(
            int winnings = default(int),
            int gameCount = default(int),
            int timePlayed = default(int),
            int winRate = default(int),
            int playerId = default(int),
            int buyin = default(int),
            int cashout = default(int))
            : base(
                winnings, 
                gameCount, 
                timePlayed, 
                winRate, 
                playerId,
                buyin,
                cashout)
        {
        }
    }
}