using Core.Entities;

namespace Core.Services.Interfaces
{
	public interface ISocialService
    {
    	void ShareResult(User user, int amount);
	}
}