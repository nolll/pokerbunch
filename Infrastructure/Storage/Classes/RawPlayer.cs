namespace Infrastructure.Storage.Classes
{
	public class RawPlayer
    {
	    public int BunchId { get; }
	    public string Slug { get; }
	    public int Id { get; }
        public int UserId { get; }
	    public string DisplayName { get; }
	    public int Role { get; }
	    public string Color { get; }

	    public RawPlayer(int bunchId, string slug, int id, int userId, string displayName, int role, string color)
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