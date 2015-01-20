using System;

namespace Infrastructure.Storage.Classes
{
	public class RawCashgame
    {
	    public int Id { get; private set; }
        public int BunchId { get; private set; }
	    public string Location { get; private set; }
	    public int Status { get; private set; }
	    public DateTime Date { get; private set; }

	    public RawCashgame(int id, int bunchId, string location, int status, DateTime date)
	    {
	        Id = id;
	        BunchId = bunchId;
	        Location = location;
	        Status = status;
	        Date = date;
	    }
    }
}