using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiChangePassword
    {
        [UsedImplicitly]
        public string OldPassword { get; set; }
        [UsedImplicitly]
        public string NewPassword { get; set; }
        [UsedImplicitly]
        public string Repeat { get; set; }

        public ApiChangePassword(string oldPassword, string newPassword, string repeat)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
            Repeat = repeat;
        }
    }
}