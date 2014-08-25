using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Application.UseCases.AddCashgameForm;
using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Add
{
    public class AddCashgamePageModel : BunchPageModel
    {
        public IEnumerable<SelectListItem> Locations { get; private set; }
        public string TypedLocation { get; private set; }
        public string SelectedLocation { get; private set; }

        public AddCashgamePageModel(BunchContextResult contextResult, CashgameOptionsResult optionsResult, AddCashgamePostModel postModel)
            : base("New Cashgame", contextResult)
        {
            Locations = GetLocationListItems(optionsResult.Locations);
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