using Core.Entities;

namespace Core.Services
{
	public interface IPlayerService
    {
        Player Get(string id);
    }
}