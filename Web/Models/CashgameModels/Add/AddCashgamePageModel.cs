using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.UseCases.AddCashgameForm;
using Core.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Add
{
    public class AddCashgamePageModel : BunchPageModel
    {
        public IEnumerable<SelectListItem> Locations { get; private set; }
        public string TypedLocation { get; private set; }
        public string SelectedLocation { get; private set; }

        public AddCashgamePageModel(BunchContextResult contextResult, AddCashgameFormResult formResult, AddCashgamePostModel postModel)
            : base("New Cashgame", contextResult)
        {
            Locations = GetLocationListItems(formResult.Locations);
            if (postModel == null) return;
            TypedLocation = postModel.TypedLocation;
            SelectedLocation = postModel.SelectedLocation;
        }

        private IEnumerable<SelectListItem> GetLocationListItems(IEnumerable<string> locations)
        {
            var listItems = locations.Select(l => new SelectListItem { Text = l, Value = l });
            var firstItem = new SelectListItem { Text = "Select Location", Value = "" };
            return new List<SelectListItem> { firstItem }.Concat(listItems);
        }
    }
}