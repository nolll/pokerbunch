using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Edit
{
    public class EditCashgamePageModel : BunchPageModel
    {
        public string IsoDate { get; }
        public string CancelUrl { get; }
        public string DeleteUrl { get; }
        public string AddLocationUrl { get; }
        public string AddEventUrl { get; }
        public IList<SelectListItem> Locations { get; }
        public string LocationId { get; }
        public IList<SelectListItem> Events { get; }
        public string EventId { get; }
        public ErrorListModel Errors { get; }

        public EditCashgamePageModel(BunchContext.Result contextResult, EditCashgameForm.Result editCashgameFormResult, EditCashgamePostModel postModel, IEnumerable<string> errors)
            : base(contextResult)
        {
            IsoDate = editCashgameFormResult.Date;
            CancelUrl = new CashgameDetailsUrl(editCashgameFormResult.Slug, editCashgameFormResult.CashgameId).Relative;
            DeleteUrl = new DeleteCashgameUrl(editCashgameFormResult.CashgameId).Relative;
            AddLocationUrl = new AddLocationUrl(editCashgameFormResult.Slug).Relative;
            AddEventUrl = new AddEventUrl(editCashgameFormResult.Slug).Relative;
            LocationId = editCashgameFormResult.LocationId;
            Locations = editCashgameFormResult.Locations.Select(o => new SelectListItem { Text = o.Name, Value = o.Id.ToString(CultureInfo.InvariantCulture) }).ToList();
            Locations.Insert(0, new SelectListItem{Text = "Select Location", Value = "0"});
            EventId = editCashgameFormResult.SelectedEventId;
            Events = editCashgameFormResult.Events.Select(o => new SelectListItem { Text = o.Name, Value = o.Id.ToString(CultureInfo.InvariantCulture) }).ToList();
            Events.Insert(0, new SelectListItem { Text = "No Event", Value = "0" }); 
            if (postModel == null) return;
            LocationId = postModel.LocationId;
            EventId = postModel.EventId;
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Edit Cashgame";

        public override View GetView()
        {
            return new View("~/Views/Pages/EditCashgame/Edit.cshtml");
        }
    }
}