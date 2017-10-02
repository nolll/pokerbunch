using Core.Entities;
using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiListUser
    {
        [UsedImplicitly]
        public string UserName { get; set; }
        [UsedImplicitly]
        public string DisplayName { get; set; }

        public ApiListUser(User user)
        {
            UserName = user.UserName;
            DisplayName = user.DisplayName;
        }

        public ApiListUser()
        {
        }
    }
}