using System.Collections.Generic;
using System.Web.Mvc;
using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add
{
    public class AddHomegamePageModel : PageModel
    {
        public List<SelectListItem> CurrencyLayoutSelectItems { get; set; }
        public List<SelectListItem> TimezoneSelectItems { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyLayout { get; set; }
        public string TimeZone { get; set; }

        public AddHomegamePageModel(AppContextResult contextResult)
            : base("Create Homegame", contextResult)
        {
        }
    }
}