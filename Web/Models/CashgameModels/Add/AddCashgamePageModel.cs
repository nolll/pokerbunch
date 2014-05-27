using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Add
{
    public class AddCashgamePageModel : AddCashgamePostModel, IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
	}
}