using Core.Classes;

namespace Application.Factories
{
	public interface IUserFactory
	{
	    User Create(
	        int id,
	        string userName,
	        string displayName,
	        string realName,
	        string email,
	        Role globalRole,
	        string token,
	        string encryptedPassword,
	        string salt);
	}
}