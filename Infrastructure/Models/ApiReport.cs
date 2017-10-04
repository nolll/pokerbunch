using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiReport
    {
        [UsedImplicitly]
        public string CashgameId { get; set; }
        [UsedImplicitly]
        public string PlayerId { get; set; }
        [UsedImplicitly]
        public int Stack { get; set; }

        public ApiReport(string cashgameId, string playerId, int stack)
        {
            CashgameId = cashgameId;
            PlayerId = playerId;
            Stack = stack;
        }
    }
}