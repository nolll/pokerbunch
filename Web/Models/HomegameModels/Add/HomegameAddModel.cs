using System;
using Core.Classes;
using Infrastructure.System;
using Web.Models.FormModels;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add{

	public class HomegameAddModel : PageModel {

	    public string DisplayName { get; set; }
	    public string Description { get; set; }
	    public string CurrencySymbol { get; set; }
	    public CurrencyLayoutFieldModel CurrencyLayoutSelectModel { get; set; }
	    public TimezoneFieldModel TimezoneSelectModel { get; set; }

		public HomegameAddModel(User user, Homegame homegame = null)
            : base (user)
        {
			if(homegame != null){
				DisplayName = homegame.DisplayName;
				Description = homegame.Description;
				SetTimezoneAndCurrency(homegame.Timezone, homegame.Currency);
			} else {
				SetTimezoneAndCurrency(Homegame.DefaultTimezone, Homegame.DefaultCurrency);
			}
		}

		private void SetTimezoneAndCurrency(TimeZoneInfo timezone, CurrencySettings currency){
			TimezoneSelectModel = GetTimezoneSelectModel(timezone);
			CurrencySymbol = currency.Symbol;
			CurrencyLayoutSelectModel = GetCurrencyLayoutSelectModel(currency.Layout);
		}

		private TimezoneFieldModel GetTimezoneSelectModel(TimeZoneInfo timezone){
			var timezones = Globalization.GetTimezones();
			return new TimezoneFieldModel("timezone", "timezone", timezone.Id, timezones, "Select Timezone");
		}

		private CurrencyLayoutFieldModel GetCurrencyLayoutSelectModel(string layout){
			return new CurrencyLayoutFieldModel("currencylayout", "currencylayout", layout);
		}

        public override string BrowserTitle
        {
            get
            {
                return "Create Homegame";
            }
        }

	}

}