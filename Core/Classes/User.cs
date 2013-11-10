namespace Core.Classes {
    public class User{

	    public int Id { get; private set; }
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string RealName { get; private set; }
        public string Email { get; private set; }
        public Role GlobalRole { get; private set; }

	    public User(int id, string userName, string displayName, string realName, string email, Role globalRole)
	    {
	        Id = id;
	        UserName = userName;
	        DisplayName = displayName;
	        RealName = realName;
	        Email = email;
	        GlobalRole = globalRole;
	    }

	    public bool IsAdmin
	    {
	        get { return GlobalRole == Role.Admin; }
	    }
	}

}