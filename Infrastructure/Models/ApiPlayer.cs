using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    public class ApiPlayer
    {
        [UsedImplicitly]
        public string BunchId { get; set; }
        [UsedImplicitly]
        public string Id { get; set; }
        [UsedImplicitly]
        public string UserId { get; set; }
        [UsedImplicitly]
        public string UserName { get; set; }
        [UsedImplicitly]
        public string Name { get; set; }
        [UsedImplicitly]
        public int RoleId { get; set; }
        [UsedImplicitly]
        public string Color { get; set; }

        public ApiPlayer(string bunchId, string userId, string userName, string name, int roleId, string color)
        {
            BunchId = bunchId;
            UserId = userId;
            UserName = userName;
            Name = name;
            RoleId = roleId;
            Color = color;
        }

        public ApiPlayer()
        {
        }
    }
}