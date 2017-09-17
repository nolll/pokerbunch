using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.UseCases;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.HomegameModels.Edit
{
    public class EditBunchPageModel : BunchPageModel
    {
        public string CancelUrl { get; }
        public string Heading { get; }
        public List<SelectListItem> CurrencyLayoutSelectItems { get; }
        public List<SelectListItem> TimezoneSelectItems { get; }
        public string Description { get; }
        public string CurrencySymbol { get; }
        public string CurrencyLayout { get; }
        public string TimeZone { get; }
        public string HouseRules { get; }
        public int DefaultBuyin { get; }
        public ErrorListModel Errors { get; }

        public EditBunchPageModel(BunchContext.Result contextResult, EditBunchForm.Result editBunchFormResult, EditBunchPostModel postModel, IEnumerable<string> errors)
            : base(contextResult)
        {
            CancelUrl = new BunchDetailsUrl(editBunchFormResult.Slug).Relative;
            Heading = editBunchFormResult.Heading;
            Description = editBunchFormResult.Description;
            HouseRules = editBunchFormResult.HouseRules;
            DefaultBuyin = editBunchFormResult.DefaultBuyin;
            TimeZone = editBunchFormResult.TimeZoneId;
            TimezoneSelectItems = editBunchFormResult.TimeZones.Select(CreateTimezoneSelectListItem).ToList();
            CurrencySymbol = editBunchFormResult.CurrencySymbol;
            CurrencyLayout = editBunchFormResult.CurrencyLayout;
            CurrencyLayoutSelectItems = editBunchFormResult.CurrencyLayouts.Select(CreateCurrencyLayoutSelectListItem).ToList();
            //CashgamesEnabled = homegame.CashgamesEnabled,
            //TournamentsEnabled = homegame.TournamentsEnabled,
            //VideosEnabled = homegame.VideosEnabled
            if (postModel == null) return;
            Description = postModel.Description;
            TimeZone = postModel.TimeZone;
            CurrencySymbol = postModel.CurrencySymbol;
            CurrencyLayout = postModel.CurrencyLayout;
            Errors = new ErrorListModel(errors);
        }

        private SelectListItem CreateTimezoneSelectListItem(AddBunchForm.TimeZoneItem item)
        {
            return CreateSelectListItem(item.Name, item.Id);
        }

        private SelectListItem CreateCurrencyLayoutSelectListItem(string item)
        {
            return CreateSelectListItem(item, item);
        }

        private SelectListItem CreateSelectListItem(string text, string val)
        {
            return new SelectListItem
            {
                Text = text,
                Value = val
            };
        }

        public override string BrowserTitle => "Edit Bunch";

        public override View GetView()
        {
            return new View("~/Views/Pages/EditBunch/Edit.cshtml");
        }
    }
}