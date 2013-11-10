namespace Core.Classes{
    public class Player{

	    public int Id { get; private set; }
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public Role Role { get; private set; }

	    public Player(int id, string userName, string displayName, Role role)
	    {
	        Id = id;
	        UserName = userName;
	        DisplayName = displayName;
	        Role = role;
	    }
	}

}