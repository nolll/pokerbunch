using Core.Entities;
using Tests.Common.Builders;

namespace Tests.Common.FakeClasses
{
    public class  CashgameTotalResultInTest : CashgameTotalResult
    {
        public CashgameTotalResultInTest(
            int winnings = 0,
            int gameCount = 0,
            int timePlayed = 0,
            int winRate = 0,
            Player player = null,
            int buyin = 0,
            int cashout = 0)
            : base(
                winnings, 
                gameCount, 
                timePlayed, 
                winRate, 
                player ?? new PlayerBuilder().Build(),
                buyin,
                cashout)
        {
        }
    }
}