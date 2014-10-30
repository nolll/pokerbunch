using Core.Entities;

namespace Infrastructure.Storage.Classes
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

        public static RawUser Create(User user)
        {
            return new RawUser(
                user.Id,
                user.UserName,
                user.DisplayName,
                user.RealName,
                user.Email,
                (int)user.GlobalRole,
                user.EncryptedPassword,
                user.Salt);
        }

	    public static User CreateReal(RawUser rawUser)
        {
            return new User(
                rawUser.Id,
                rawUser.UserName,
                rawUser.DisplayName,
                rawUser.RealName,
                rawUser.Email,
                (Role)rawUser.GlobalRole,
                rawUser.EncryptedPassword,
                rawUser.Salt);
        }

	    public static string ToStringId(int id)
	    {
	        return string.Concat("RawUsers/", id);
	    }
    }
}