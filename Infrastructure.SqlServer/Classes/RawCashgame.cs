using System;

namespace Infrastructure.SqlServer.Classes
{
	public class RawCashgame
    {
	    public int Id { get; set; }
        public int BunchId { get; set; }
	    public string Location { get; set; }
	    public int Status { get; set; }
	    public DateTime Date { get; set; }
	}
}