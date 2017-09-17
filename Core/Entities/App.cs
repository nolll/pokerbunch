namespace Core.Entities
{
    public class App : IEntity
    {
        public string Id { get; }
        public string AppKey { get; }
        public string Name { get; }
        public string UserId { get; }
        public string CacheId => Id;

        public App(string id, string appKey, string name, string userId)
        {
            Id = id;
            AppKey = appKey;
            Name = name;
            UserId = userId;
        }
    }
}