namespace Infrastructure.Data.Classes
{
	public class RawUser
    {
	    public int Id { get; set; }
	    public string UserName { get; set; }
	    public string DisplayName { get; set; }
	    public string RealName { get; set; }
	    public string Email { get; set; }
	    public int GlobalRole { get; set; }
	    public string EncryptedPassword { get; set; }
	    public string Salt { get; set; }

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