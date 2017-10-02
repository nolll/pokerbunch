using JetBrains.Annotations;

namespace Infrastructure.Api.Services
{
    internal class ApiInvite
    {
        [UsedImplicitly]
        public string Email { get; set; }

        public ApiInvite(string email)
        {
            Email = email;
        }
    }
}