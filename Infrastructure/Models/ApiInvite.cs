using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiInvite
    {
        [UsedImplicitly]
        public string PlayerId { get; set; }

        [UsedImplicitly]
        public string Email { get; set; }

        public ApiInvite(string playerId, string email)
        {
            PlayerId = playerId;
            Email = email;
        }
    }
}