using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    internal class ApiApp
    {
        [UsedImplicitly]
        public string Id { get; set; }
        [UsedImplicitly]
        public string Key { get; set; }
        [UsedImplicitly]
        public string Name { get; set; }
        [UsedImplicitly]
        public string UserId { get; set; }

        public ApiApp(string id, string key, string name, string userId)
        {
            Id = id;
            Key = key;
            Name = name;
            UserId = userId;
        }

        public ApiApp()
        {
        }
    }
}