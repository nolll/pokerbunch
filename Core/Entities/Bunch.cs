using System;

namespace Core.Entities
{
    public class Bunch : IEntity
    {
	    public int Id { get; }
	    public string Slug { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public string HouseRules { get; }
        public TimeZoneInfo Timezone { get; }
	    public int DefaultBuyin { get; }
	    public Currency Currency { get; }
	    public bool CashgamesEnabled { get; }
        public bool TournamentsEnabled { get; }
        public bool VideosEnabled { get; }

        public Bunch(
            int id, 
            string slug, 
            string displayName, 
            string description, 
            string houseRules, 
            TimeZoneInfo timezone, 
            int defaultBuyin, 
            Currency currency)
	    {
	        Id = id;
	        Slug = slug;
	        DisplayName = displayName;
	        Description = description;
	        HouseRules = houseRules;
	        Timezone = timezone;
	        DefaultBuyin = defaultBuyin;
	        Currency = currency;
	        CashgamesEnabled = true;
	        TournamentsEnabled = false;
	        VideosEnabled = false;
	    }
	}
}
