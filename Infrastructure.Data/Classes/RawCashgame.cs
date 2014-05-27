using System;

namespace Infrastructure.Data.Classes
{
	public class RawCashgame
    {
	    public int Id { get; set; }
        public int HomegameId { get; set; }
	    public string Location { get; set; }
	    public int Status { get; set; }
	    public DateTime Date { get; set; }
	}
}