using System;

namespace Infrastructure.Storage.Classes
{
	public class RawCashgame
    {
	    public string Id { get; private set; }
	    public string BunchId { get; private set; }
	    public string LocationId { get; private set; }
	    public int Status { get; private set; }
	    public DateTime Date { get; private set; }

	    public RawCashgame(string id, string bunchId, string locationId, int status, DateTime date)
	    {
	        Id = id;
	        BunchId = bunchId;
	        LocationId = locationId;
	        Status = status;
	        Date = date;
	    }
    }
}