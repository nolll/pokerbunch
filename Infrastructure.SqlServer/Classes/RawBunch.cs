namespace Infrastructure.SqlServer.Classes {

	public class RawBunch{

	    public int Id { get; set; }
	    public string Slug { get; set; }
	    public string DisplayName { get; set; }
	    public string Description { get; set; }
        public string HouseRules { get; set; }
        public string TimezoneName { get; set; }
	    public int DefaultBuyin { get; set; }
	    public string CurrencyLayout { get; set; }
	    public string CurrencySymbol { get; set; }
        public bool CashgamesEnabled { get; set; }
        public bool TournamentsEnabled { get; set; }
        public bool VideosEnabled { get; set; }

	}

}
