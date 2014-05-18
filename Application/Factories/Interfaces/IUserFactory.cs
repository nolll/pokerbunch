using Core.Entities;

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
	        string encryptedPassword,
	        string salt);
	}
}