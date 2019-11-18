using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Add
{
    public class AddCashgamePageModel : BunchPageModel
    {
        public IEnumerable<SelectListItem> Locations { get; }
        public string LocationId { get; }
        public ErrorListModel Errors { get; }

        public AddCashgamePageModel(AppSettings appSettings, BunchContext.Result contextResult, AddCashgameForm.Result formResult, AddCashgamePostModel postModel, IEnumerable<string> errors)
            : base(appSettings, contextResult)
        {
            Locations = GetLocationListItems(formResult.Locations);
            if (postModel == null) return;
            LocationId = postModel.LocationId;
            Errors = new ErrorListModel(errors);
        }

        private IEnumerable<SelectListItem> GetLocationListItems(IEnumerable<AddCashgameForm.LocationItem> locations)
        {
            var listItems = locations.Select(o => new SelectListItem { Text = o.Name, Value = o.Id.ToString(CultureInfo.InvariantCulture) }).ToList();
            var firstItem = new SelectListItem { Text = "Select Location", Value = "" };
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