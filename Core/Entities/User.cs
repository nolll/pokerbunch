namespace Core.Entities
{
    public class User : IEntity
    {
	    public int Id { get; }
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string RealName { get; private set; }
        public string Email { get; private set; }
        public Role GlobalRole { get; }
        public string EncryptedPassword { get; private set; }
        public string Salt { get; private set; }

	    public User(
            int id, 
            string userName, 
            string displayName, 
            string realName, 
            string email, 
            Role globalRole,
            string encryptedPassword,
            string salt)
	    {
	        Id = id;
	        UserName = userName;
	        DisplayName = displayName;
	        RealName = realName;
	        Email = email;
	        GlobalRole = globalRole;
	        EncryptedPassword = encryptedPassword;
	        Salt = salt;
	    }

	    public bool IsAdmin => GlobalRole == Role.Admin;

        public void SetPassword(string encryptedPassword, string salt)
        {
            EncryptedPassword = encryptedPassword;
            Salt = salt;
        }
	}

}