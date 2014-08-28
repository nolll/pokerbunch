using System.Collections.Generic;
using System.Web.Mvc;
using Application.Urls;
using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Edit
{
    public class BunchEditPageModel : BunchPageModel
    {
        public Url CancelUrl { get; set; }
        public string Heading { get; set; }
        public List<SelectListItem> CurrencyLayoutSelectItems { get; set; }
        public List<SelectListItem> TimezoneSelectItems { get; set; }
        public string Description { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyLayout { get; set; }
        public string TimeZone { get; set; }
        public string HouseRules { get; set; }
        public int DefaultBuyin { get; set; }

        public BunchEditPageModel(BunchContextResult contextResult)
            : base("Edit Bunch", contextResult)
        {
        }
    }
}