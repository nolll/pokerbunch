using Core.Entities;

namespace Infrastructure.Storage.Classes
{
	public class RawPlayer
    {
	    public int BunchId { get; private set; }
        public int Id { get; private set; }
        public int UserId { get; private set; }
	    public string DisplayName { get; private set; }
	    public int Role { get; private set; }

	    public RawPlayer(int bunchId, int id, int userId, string displayName, int role)
	    {
	        BunchId = bunchId;
	        Id = id;
	        UserId = userId;
	        DisplayName = displayName;
	        Role = role;
	    }

	    public bool IsUser
	    {
	        get { return UserId != default(int); }
	    }

	    public static RawPlayer Create(Player player)
	    {
	        return new RawPlayer(
	            player.BunchId,
                player.Id,
                player.UserId,
                player.DisplayName,
                (int)player.Role);
	    }
	}
}