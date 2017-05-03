namespace Core.Entities
{
    public class User : IEntity
    {
	    public string Id { get; }
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string RealName { get; private set; }
        public string Email { get; private set; }
        public Role Role { get; }
        public string EncryptedPassword { get; private set; }
        public string Salt { get; private set; }
        public string CacheId => Id;

        public User(
            string id, 
            string userName, 
            string displayName = null, 
            string realName = null, 
            string email = null, 
            Role role = Role.Player,
            string encryptedPassword = null,
            string salt = null)
	    {
	        Id = id;
	        UserName = userName;
	        DisplayName = displayName ?? string.Empty;
	        RealName = realName ?? string.Empty;
	        Email = email ?? string.Empty;
	        Role = role;
	        EncryptedPassword = encryptedPassword ?? string.Empty;
	        Salt = salt ?? string.Empty;
	    }

	    public bool IsAdmin => Role == Role.Admin;

        public void SetPassword(string encryptedPassword, string salt)
        {
            EncryptedPassword = encryptedPassword;
            Salt = salt;
        }
	}

}