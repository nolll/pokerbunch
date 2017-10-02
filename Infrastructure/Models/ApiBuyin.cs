using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiBuyin
    {
        [UsedImplicitly]
        public string PlayerId { get; set; }
        [UsedImplicitly]
        public int Added { get; set; }
        [UsedImplicitly]
        public int Stack { get; set; }

        public ApiBuyin(string playerId, int added, int stack)
        {
            PlayerId = playerId;
            Added = added;
            Stack = stack;
        }
    }
}