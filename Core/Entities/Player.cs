namespace Core.Entities
{
    public class Player : IEntity
    {
        private const string DefaultColor = "#9e9e9e";

        public string BunchId { get; }
        public string Id { get; }
        public string UserId { get; }
        public string UserName { get; }
        public string DisplayName { get; }
        public Role Role { get; }
        public string Color { get; }
        public bool IsUser => !string.IsNullOrEmpty(UserId);
        public string CacheId => Id;

        public Player(
            string bunchId,
            string id,
            string userId,
            string userName, 
            string displayName = null, 
            Role role = Role.Player,
            string color = null)
        {
	        BunchId = bunchId;
	        Id = id;
	        UserId = userId;
            UserName = userName;
	        DisplayName = displayName;
	        Role = role;
	        Color = color ?? DefaultColor;
	    }

        public static Player NewWithoutUser(string bunchId, string displayName, Role role = Role.Player, string color = null)
        {
            return new Player(bunchId, "", "", "", displayName, role, color);
        }

        public static Player NewWithUser(string bunchId, string userId, string userName, Role role = Role.Player, string color = null)
        {
            return new Player(bunchId, "", userId, userName, null, role, color);
        }

        public bool IsInRole(Role requiredRole)
        {
            return Role >= requiredRole;
        }
	}
}