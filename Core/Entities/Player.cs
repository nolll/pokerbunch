namespace Core.Entities
{
    public class Player : IEntity
    {
        private const string DefaultColor = "#9e9e9e";

        public string BunchId { get; private set; }
        public string Slug { get; }
        public string Id { get; }
        public string UserId { get; }
        public string DisplayName { get; private set; }
        public Role Role { get; }
        public string Color { get; private set; }
        public bool IsUser => !string.IsNullOrEmpty(UserId);
        public string CacheId => Id;

        public Player(
            string bunchId,
            string slug,
            string id,
            string userId, 
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

        public static Player NewWithoutUser(string bunchId, string slug, string displayName, Role role = Role.Player, string color = null)
        {
            return new Player(bunchId, slug, "", "", displayName, role, color);
        }

        public static Player NewWithUser(string bunchId, string slug, string userId, Role role = Role.Player, string color = null)
        {
            return new Player(bunchId, slug, "", userId, null, role, color);
        }

        public bool IsInRole(Role requiredRole)
        {
            return Role >= requiredRole;
        }
	}
}