/*
using System;

namespace Core.Classes{

	class Homegame{

	    public int Id { get; set; }
	    public string Slug { get; set; }
	    public string DisplayName { get; set; }
	    public string Description { get; set; }
	    public string HouseRules { get; set; }
	    public TimeZone Timezone { get; set; }
	    public int DefaultBuyin { get; set; }
	    public CurrencySettings Currency { get; set; }

	    public Homegame()
	    {
	        
	    }

		public function __construct(){
			locations = array();
			setCurrency(self::getDefaultCurrency());
			setTimezone(self::getDefaultTimezone());
			defaultBuyin = 0;
			cashgamesEnabled = true;
			tournamentsEnabled = true;
			videosEnabled = false;
		}

		public static TimeZone GetDefaultTimezone(){
			return new DateTimeZone('UTC');
		}

		public static CurrencySettings getDefaultCurrency(){
			return new CurrencySettings("$", "{SYMBOL}{AMOUNT}");
		}

	}

}
*/