namespace Core.Entities
{
    public class Player : IEntity
    {
        public int BunchId { get; private set; }
	    public int Id { get; private set; }
        public int UserId { get; private set; }
        public string DisplayName { get; private set; }
        public Role Role { get; private set; }
        public string Color { get; private set; }

	    public Player(
            int bunchId,
            int id, 
            int userId, 
            string displayName, 
            Role role,
            string color)
	    {
	        BunchId = bunchId;
	        Id = id;
	        UserId = userId;
	        DisplayName = displayName;
	        Role = role;
	        Color = color;
	    }

        public Player(int bunchId, string displayName, Role role, string color)
            : this(bunchId, 0, 0, displayName, role, color)
        {
        }

        public Player(int bunchId, int userId, Role role, string color)
            : this(bunchId, 0, userId, null, role, color)
        {
        }

        public bool IsUser {
            get { return UserId != default(int); }
        }

        public bool IsInRole(Role requiredRole)
        {
            return Role >= requiredRole;
        }
	}
}