using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add{

	public class AddHomegamePageModel : PageModel {

	    public string DisplayName { get; set; }
	    public string Description { get; set; }
	    public string CurrencySymbol { get; set; }
	    public string CurrencyLayout { get; set; }
        public List<SelectListItem> CurrencyLayoutSelectModel { get; set; }
        public string TimeZone { get; set; }
	    public List<SelectListItem> TimezoneSelectModel { get; set; }

        public override string BrowserTitle
        {
            get
            {
                return "Create Homegame";
            }
        }

	}

}