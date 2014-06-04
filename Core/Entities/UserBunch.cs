namespace Core.Entities
{
    public class UserBunch
    {
        public string Slug { get; private set; }
        public Role Role { get; private set; }
        public string Name { get; private set; }
        public int Id { get; set; }

        public UserBunch(string slug, Role role, string name, int id)
        {
            Slug = slug;
            Role = role;
            Name = name;
            Id = id;
        }
    }
}