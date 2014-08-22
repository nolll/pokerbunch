using System.Collections.Generic;
using System.Web.Mvc;
using Application.Urls;
using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Edit
{
    public class EditCashgamePageModel : BunchPageModel
    {
        public string IsoDate { get; set; }
        public Url CancelUrl { get; set; }
        public Url DeleteUrl { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
        public string TypedLocation { get; set; }
        public string SelectedLocation { get; set; }

        public EditCashgamePageModel(BunchContextResult contextResult)
            : base("Edit Cashgame", contextResult)
        {
        }
    }
}