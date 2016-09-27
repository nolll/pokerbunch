using Core.Services;

namespace Core.Entities
{
    public class Player : IEntity
    {
        private const string DefaultColor = "#9e9e9e";

        public int BunchId { get; private set; }
        public string Slug { get; }
        public int Id { get; }
        public int UserId { get; }
        public string DisplayName { get; private set; }
        public Role Role { get; }
        public string Color { get; private set; }
        public bool IsUser => UserId != default(int);

        public Player(
            int bunchId,
            string slug,
            int id, 
            int userId, 
            string displayName = null, 
            Role role = Role.Player,
            string color = null)
	    {
	        BunchId = bunchId;
	        Slug = slug;
	        Id = id;
	        UserId = userId;
	        DisplayName = displayName;
	        Role = role;
	        Color = color ?? DefaultColor;
	    }

        public static Player New(int bunchId, string slug, string displayName, Role role = Role.Player, string color = null)
        {
            return new Player(bunchId, slug, 0, 0, displayName, role, color);
        }

        public static Player New(int bunchId, string slug, int userId, Role role = Role.Player, string color = null)
        {
            return new Player(bunchId, slug, 0, userId, null, role, color);
        }

        public bool IsInRole(Role requiredRole)
        {
            return Role >= requiredRole;
        }
	}
}