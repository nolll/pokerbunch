using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Urls;

namespace Web.Models.HomegameModels.Edit
{
    public class EditBunchPageModel : BunchPageModel
    {
        public string CancelUrl { get; private set; }
        public string Heading { get; private set; }
        public List<SelectListItem> CurrencyLayoutSelectItems { get; private set; }
        public List<SelectListItem> TimezoneSelectItems { get; private set; }
        public string Description { get; private set; }
        public string CurrencySymbol { get; private set; }
        public string CurrencyLayout { get; private set; }
        public string TimeZone { get; private set; }
        public string HouseRules { get; private set; }
        public int DefaultBuyin { get; private set; }

        public EditBunchPageModel(BunchContext.Result contextResult, EditBunchForm.Result editBunchFormResult, EditBunchPostModel postModel)
            : base("Edit Bunch", contextResult)
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
    }
}