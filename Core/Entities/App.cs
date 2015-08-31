namespace Core.Entities
{
    public class App
    {
        public int Id { get; set; }
        public string AppKey { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

        public App(int id, string appKey, string name, int userId)
        {
            Id = id;
            AppKey = appKey;
            Name = name;
            UserId = userId;
        }
    }
}