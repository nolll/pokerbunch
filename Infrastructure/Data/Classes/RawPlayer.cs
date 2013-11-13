namespace Infrastructure.Data.Classes{

	public class RawPlayer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
	    public string DisplayName { get; set; }
	    public int Role { get; set; }

	    public bool IsUser
	    {
	        get { return UserId != default(int); }
	    }
	}

}