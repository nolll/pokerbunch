using System.Collections.Generic;
using System.Web.Mvc;
using Application.Urls;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Edit
{
    public class EditCashgamePageModel : CashgameEditPostModel, IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public string IsoDate { get; set; }
        public bool EnableDelete { get; set; }
        public Url CancelUrl { get; set; }
        public Url DeleteUrl { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
    }
}