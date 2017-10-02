using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiReport
    {
        [UsedImplicitly]
        public string PlayerId { get; set; }
        [UsedImplicitly]
        public int Stack { get; set; }

        public ApiReport(string playerId, int stack)
        {
            PlayerId = playerId;
            Stack = stack;
        }
    }
}