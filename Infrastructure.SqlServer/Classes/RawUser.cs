namespace Infrastructure.SqlServer.Classes
{
	public class RawUser
    {
	    public int Id { get; private set; }
	    public string UserName { get; private set; }
	    public string DisplayName { get; private set; }
	    public string RealName { get; private set; }
	    public string Email { get; private set; }
	    public int GlobalRole { get; private set; }
	    public string EncryptedPassword { get; private set; }
	    public string Salt { get; private set; }

	    public RawUser(int id, string userName, string displayName, string realName, string email, int globalRole, string encryptedPassword, string salt)
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
	}
}