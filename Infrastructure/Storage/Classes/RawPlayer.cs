namespace Infrastructure.Storage.Classes
{
	public class RawPlayer
    {
	    public string BunchId { get; }
	    public string Id { get; }
        public string UserId { get; }
	    public string DisplayName { get; }
	    public int Role { get; }
	    public string Color { get; }

	    public RawPlayer(string bunchId, string id, string userId, string displayName, int role, string color)
	    {
	        BunchId = bunchId;
	        Id = id;
	        UserId = userId;
	        DisplayName = displayName;
	        Role = role;
	        Color = color;
	    }
	}
}