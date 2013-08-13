using System;

namespace Core.Classes{

	class Homegame{

	    public int Id { get; set; }
	    public string Slug { get; set; }
	    public string DisplayName { get; set; }
	    public string Description { get; set; }
	    public string HouseRules { get; set; }
        public TimeZoneInfo Timezone { get; set; }
	    public int DefaultBuyin { get; set; }
	    public CurrencySettings Currency { get; set; }
	    public bool CashgamesEnabled { get; set; }
        public bool TournamentsEnabled { get; set; }
        public bool VideosEnabled { get; set; }

	    public Homegame()
	    {
            Currency = DefaultCurrency;
            Timezone = DefaultTimezone;
            DefaultBuyin = 0;
            CashgamesEnabled = true;
            TournamentsEnabled = false;
            VideosEnabled = false;
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
