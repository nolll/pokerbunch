namespace Core.Entities
{
    public class App : IEntity
    {
        public string Id { get; }
        public string AppKey { get; private set; }
        public string Name { get; private set; }
        public string UserId { get; private set; }
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