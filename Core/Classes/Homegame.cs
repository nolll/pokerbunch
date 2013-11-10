using System;

namespace Core.Classes{
    public class Homegame{

	    public int Id { get; private set; }
	    public string Slug { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        public string HouseRules { get; private set; }
        public TimeZoneInfo Timezone { get; private set; }
	    public int DefaultBuyin { get; private set; }
	    public CurrencySettings Currency { get; private set; }
	    public bool CashgamesEnabled { get; private set; }
        public bool TournamentsEnabled { get; private set; }
        public bool VideosEnabled { get; private set; }

	    public Homegame(
            int id, 
            string slug, 
            string displayName, 
            string description, 
            string houseRules, 
            TimeZoneInfo timezone, 
            int defaultBuyin, 
            CurrencySettings currency
            )
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

	    private Homegame()
	    {
            Currency = DefaultCurrency;
            Timezone = DefaultTimezone;
            DefaultBuyin = 0;
	    }

		public static TimeZoneInfo DefaultTimezone{
            get
            {
                return TimeZoneInfo.Utc;
            }
		}

		public static CurrencySettings DefaultCurrency{
            get 
            {
                return new CurrencySettings("$", "{SYMBOL}{AMOUNT}");
            }
		}

	}

}
