using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add
{
    public class AddHomegamePageModel : AddHomegamePostModel, IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public List<SelectListItem> CurrencyLayoutSelectItems { get; set; }
        public List<SelectListItem> TimezoneSelectItems { get; set; }
    }
}