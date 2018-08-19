namespace Core.Entities
{
    public class App 
    {
        public string Id { get; }
        public string AppKey { get; }
        public string Name { get; }
        public string UserId { get; }

        public App(string id, string appKey, string name, string userId)
        {
            Id = id;
            AppKey = appKey;
            Name = name;
            UserId = userId;
        }
    }
}