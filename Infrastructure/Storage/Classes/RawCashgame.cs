using System;

namespace Infrastructure.Storage.Classes
{
	public class RawCashgame
    {
	    public int Id { get; private set; }
	    public string Slug { get; private set; }
	    public int BunchId { get; private set; }
	    public int LocationId { get; private set; }
	    public int Status { get; private set; }
	    public DateTime Date { get; private set; }

	    public RawCashgame(int id, string slug, int bunchId, int locationId, int status, DateTime date)
	    {
	        Id = id;
	        Slug = slug;
	        BunchId = bunchId;
	        LocationId = locationId;
	        Status = status;
	        Date = date;
	    }
    }
}