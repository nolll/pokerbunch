using Core.Entities;
using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiUser
    {
        [UsedImplicitly]
        public string Id { get; set; }
        [UsedImplicitly]
        public string UserName { get; set; }
        [UsedImplicitly]
        public string DisplayName { get; set; }
        [UsedImplicitly]
        public string RealName { get; set; }
        [UsedImplicitly]
        public string Email { get; set; }
        [UsedImplicitly]
        public string Role { get; set; }
        [UsedImplicitly]
        public string Password { get; set; }

        public ApiUser(User user, string password = null)
        {
            Id = user.Id;
            UserName = user.UserName;
            DisplayName = user.DisplayName;
            RealName = user.RealName;
            Email = user.Email;
            Role = user.Role.ToString();
            Password = password;
        }

        public ApiUser()
        {
        }
    }
}