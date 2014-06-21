using Application.UseCases.CashgameTopList;
using Core.Entities;

namespace Tests.Common.FakeClasses
{
    public class TopListItemInTest : TopListItem
    {
        public TopListItemInTest(
            int rank = 0,
            int playerId = 0,
            string name = null,
            Money winnings = null,
            Money buyin = null, 
            Money cashout = null, 
            Time timePlayed = null, 
            int gamesPlayed = 0, 
            Money winRate = null)
            
            : base(
                rank, 
                playerId, 
                name, 
                winnings, 
                buyin, 
                cashout, 
                timePlayed, 
                gamesPlayed, 
                winRate)
        {
        }
    }
}