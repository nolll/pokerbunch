namespace Core.Entities
{
    public class User
    {
	    public string Id { get; }
        public string UserName { get; }
        public string DisplayName { get; }
        public string RealName { get; }
        public string Email { get; }
        public Role Role { get; }

        public User(
            string id, 
            string userName, 
            string displayName = null, 
            string realName = null, 
            string email = null, 
            Role role = Role.Player)
	    {
	        Id = id;
	        UserName = userName;
	        DisplayName = displayName ?? string.Empty;
	        RealName = realName ?? string.Empty;
	        Email = email ?? string.Empty;
	        Role = role;
	    }

	    public bool IsAdmin => Role == Role.Admin;
	}
}