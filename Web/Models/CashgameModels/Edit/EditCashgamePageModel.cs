using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Urls.SiteUrls;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Edit
{
    public class EditCashgamePageModel : BunchPageModel
    {
        public string IsoDate { get; private set; }
        public string CancelUrl { get; private set; }
        public string DeleteUrl { get; private set; }
        public IList<SelectListItem> Locations { get; private set; }
        public int SelectedLocation { get; private set; }
        public IList<SelectListItem> Events { get; private set; }
        public int SelectedEvent { get; private set; }

        public EditCashgamePageModel(BunchContext.Result contextResult, EditCashgameForm.Result editCashgameFormResult, EditCashgamePostModel postModel)
            : base("Edit Cashgame", contextResult)
        {
            IsoDate = editCashgameFormResult.Date;
            CancelUrl = new CashgameDetailsUrl(editCashgameFormResult.CashgameId).Relative;
            DeleteUrl = new DeleteCashgameUrl(editCashgameFormResult.CashgameId).Relative;
            SelectedLocation = editCashgameFormResult.LocationId;
            Locations = editCashgameFormResult.Locations.Select(o => new SelectListItem { Text = o.Name, Value = o.Id.ToString(CultureInfo.InvariantCulture) }).ToList();
            Locations.Insert(0, new SelectListItem{Text = "Select Location", Value = "0"});
            SelectedEvent = editCashgameFormResult.SelectedEventId;
            Events = editCashgameFormResult.Events.Select(o => new SelectListItem { Text = o.Name, Value = o.Id.ToString(CultureInfo.InvariantCulture) }).ToList();
            Events.Insert(0, new SelectListItem { Text = "No Event", Value = "0" }); 
            if (postModel == null) return;
            SelectedLocation = postModel.LocationId;
            SelectedEvent = postModel.EventId;
        }
    }
}