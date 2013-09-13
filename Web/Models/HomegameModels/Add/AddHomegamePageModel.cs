using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add{

    public class AddHomegamePostModel
    {
        [Required(ErrorMessage = "Display Name can't be empty")]
        public string DisplayName { get; set; }

        public string Description { get; set; }
        
        [Required(ErrorMessage = "Currency Symbol can't be empty")]
        public string CurrencySymbol { get; set; }
        
        [Required(ErrorMessage = "Currency Layout can't be empty")]
        public string CurrencyLayout { get; set; }

        [Required(ErrorMessage = "Timezone can't be empty")]
        public string TimeZone { get; set; }
    }

	public class AddHomegamePageModel : AddHomegamePostModel, IPageModel {

        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public List<SelectListItem> CurrencyLayoutSelectModel { get; set; }
	    public List<SelectListItem> TimezoneSelectModel { get; set; }
	}

}