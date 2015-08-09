using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.UseCases;
using Core.UseCases.EditCashgameForm;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Edit
{
    public class EditCashgamePageModel : BunchPageModel
    {
        public string IsoDate { get; private set; }
        public string CancelUrl { get; private set; }
        public string DeleteUrl { get; private set; }
        public IEnumerable<SelectListItem> Locations { get; private set; }
        public string TypedLocation { get; private set; }
        public string SelectedLocation { get; private set; }

        public EditCashgamePageModel(BunchContext.Result contextResult, EditCashgameFormResult editCashgameFormResult, EditCashgamePostModel postModel)
            : base("Edit Cashgame", contextResult)
        {
            IsoDate = editCashgameFormResult.Date;
            CancelUrl = editCashgameFormResult.CancelUrl.Relative;
            DeleteUrl = editCashgameFormResult.DeleteUrl.Relative;
            TypedLocation = editCashgameFormResult.Location;
            SelectedLocation = editCashgameFormResult.Location;
            Locations = editCashgameFormResult.Locations.Select(l => new SelectListItem {Text = l, Value = l});
            if (postModel == null) return;
            TypedLocation = postModel.TypedLocation;
            SelectedLocation = postModel.SelectedLocation;
        }
    }
}