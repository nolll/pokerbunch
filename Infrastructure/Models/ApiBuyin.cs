using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiBuyin
    {
        [UsedImplicitly]
        public string CashgameId { get; set; }
        [UsedImplicitly]
        public string PlayerId { get; set; }
        [UsedImplicitly]
        public int Added { get; set; }
        [UsedImplicitly]
        public int Stack { get; set; }

        public ApiBuyin(string cashgameId, string playerId, int added, int stack)
        {
            CashgameId = cashgameId;
            PlayerId = playerId;
            Added = added;
            Stack = stack;
        }
    }
}