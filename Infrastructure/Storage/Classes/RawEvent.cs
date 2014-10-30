namespace Infrastructure.Storage.Classes
{
	public class RawEvent
    {
	    public int Id { get; private set; }
	    public string Name { get; private set; }

	    public RawEvent(int id, string name)
	    {
	        Id = id;
	        Name = name;
	    }
	}
}