namespace Core.Entities
{
    public class UserBunch
    {
        public string Slug { get; private set; }
        public Role Role { get; private set; }
        public string Name { get; private set; }
        public int Id { get; private set; }

        public UserBunch(string slug, Role role, string name, int playerId)
        {
            Slug = slug;
            Role = role;
            Name = name;
            Id = playerId;
        }
    }
}