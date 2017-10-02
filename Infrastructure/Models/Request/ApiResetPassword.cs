using JetBrains.Annotations;

namespace Infrastructure.Api.Models.Request
{
    public class ApiResetPassword
    {
        [UsedImplicitly]
        public string Email { get; set; }

        public ApiResetPassword(string email)
        {
            Email = email;
        }
    }
}