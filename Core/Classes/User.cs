namespace Core.Classes {

	class User{

	    public int Id { get; set; }
	    public string UserName { get; set; }
	    public string DisplayName { get; set; }
	    public string RealName { get; set; }
	    public string Email { get; set; }
	    public Role GlobalRole { get; set; }

	    public User()
	    {
	        GlobalRole = Role.None;
	    }

	    public bool IsAdmin
	    {
	        get { return GlobalRole == Role.Admin; }
	    }
	}

}