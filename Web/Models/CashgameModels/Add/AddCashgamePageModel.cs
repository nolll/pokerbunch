using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Add
{
    public class AddCashgamePageModel : BunchPageModel
    {
        public IEnumerable<SelectListItem> Locations { get; private set; }
        public string LocationId { get; private set; }
        public IEnumerable<SelectListItem> Events { get; private set; }
        public string EventId { get; private set; }

        public AddCashgamePageModel(BunchContext.Result contextResult, AddCashgameForm.Result formResult, AddCashgamePostModel postModel)
            : base(contextResult)
        {
            Locations = GetLocationListItems(formResult.Locations);
            Events = GetEventListItems(formResult.Events);
            if (postModel == null) return;
            LocationId = postModel.LocationId;
            EventId = postModel.EventId;
        }

        private IEnumerable<SelectListItem> GetLocationListItems(IEnumerable<AddCashgameForm.LocationItem> locations)
        {
            var listItems = locations.Select(o => new SelectListItem { Text = o.Name, Value = o.Id.ToString(CultureInfo.InvariantCulture) }).ToList();
            var firstItem = new SelectListItem { Text = "Select Location", Value = "" };
            listItems.Insert(0, firstItem);
            return listItems;
        }

        private IEnumerable<SelectListItem> GetEventListItems(IEnumerable<AddCashgameForm.EventItem> locations)
        {
            var listItems = locations.Select(o => new SelectListItem { Text = o.Name, Value = o.Id.ToString(CultureInfo.InvariantCulture) }).ToList();
            var firstItem = new SelectListItem { Text = "No Event", Value = "" };
            listItems.Insert(0, firstItem);
            return listItems;
        }

        public override string BrowserTitle => "Start Cashgame";

        public override View GetView()
        {
            return new View("~/Views/Pages/AddCashgame/Add.cshtml");
        }
    }
}