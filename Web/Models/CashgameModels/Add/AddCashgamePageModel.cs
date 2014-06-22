using System.Collections.Generic;
using System.Web.Mvc;
using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Add
{
    public class AddCashgamePageModel : PageModel
    {
        public IEnumerable<SelectListItem> Locations { get; set; }
        public string TypedLocation { get; private set; }
        public string SelectedLocation { get; private set; }

        public AddCashgamePageModel(BunchContextResult contextResult, AddCashgamePostModel postModel)
            : base("New Cashgame", contextResult)
        {
            if (postModel == null) return;
            TypedLocation = postModel.TypedLocation;
            SelectedLocation = postModel.SelectedLocation;
        }
    }
}