using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiCashout
    {
        [UsedImplicitly]
        public string PlayerId { get; set; }
        [UsedImplicitly]
        public int Stack { get; set; }

        public ApiCashout(string playerId, int stack)
        {
            PlayerId = playerId;
            Stack = stack;
        }
    }
}