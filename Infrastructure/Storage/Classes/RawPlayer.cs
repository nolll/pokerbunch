namespace Infrastructure.Storage.Classes
{
	public class RawPlayer
    {
	    public string BunchId { get; }
	    public string Slug { get; }
	    public string Id { get; }
        public string UserId { get; }
	    public string DisplayName { get; }
	    public int Role { get; }
	    public string Color { get; }

	    public RawPlayer(string bunchId, string slug, string id, string userId, string displayName, int role, string color)
	    {
	        BunchId = bunchId;
	        Slug = slug;
	        Id = id;
	        UserId = userId;
	        DisplayName = displayName;
	        Role = role;
	        Color = color;
	    }
	}
}