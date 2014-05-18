using Core.Entities;

namespace Application.Services
{
	public interface ISocialService
    {
    	void ShareResult(User user, int amount);
	}
}