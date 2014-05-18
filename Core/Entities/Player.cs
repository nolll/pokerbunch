namespace Core.Entities
{
    public class Player : ICacheable
    {
	    public int Id { get; private set; }
        public int UserId { get; private set; }
        public string DisplayName { get; private set; }
        public Role Role { get; private set; }

	    public Player(
            int id, 
            int userId, 
            string displayName, 
            Role role)
	    {
	        Id = id;
	        UserId = userId;
	        DisplayName = displayName;
	        Role = role;
	    }

        public bool IsUser {
            get { return UserId != default(int); }
        }
	}

}