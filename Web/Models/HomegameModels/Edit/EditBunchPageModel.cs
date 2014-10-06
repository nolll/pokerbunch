using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.UseCases.BunchContext;
using Core.UseCases.EditBunchForm;
using Web.Models.PageBaseModels;

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

        public EditBunchPageModel(BunchContextResult contextResult, EditBunchFormResult editBunchFormResult, EditBunchPostModel postModel)
            : base("Edit Bunch", contextResult)
        {
            CancelUrl = editBunchFormResult.CancelUrl.Relative;
            Heading = editBunchFormResult.Heading;
            Description = editBunchFormResult.Description;
            HouseRules = editBunchFormResult.HouseRules;
            DefaultBuyin = editBunchFormResult.DefaultBuyin;
            TimeZone = editBunchFormResult.TimeZoneId;
            TimezoneSelectItems = GetTimezoneSelectModel();
            CurrencySymbol = editBunchFormResult.CurrencySymbol;
            CurrencyLayout = editBunchFormResult.CurrencyLayout;
            CurrencyLayoutSelectItems = GetCurrencyLayoutSelectModel();
            //CashgamesEnabled = homegame.CashgamesEnabled,
            //TournamentsEnabled = homegame.TournamentsEnabled,
            //VideosEnabled = homegame.VideosEnabled
            if (postModel == null) return;
            Description = postModel.Description;
            TimeZone = postModel.TimeZone;
            CurrencySymbol = postModel.CurrencySymbol;
            CurrencyLayout = postModel.CurrencyLayout;
        }

        // todo: remove this and put the data in the result
        private List<SelectListItem> GetTimezoneSelectModel()
        {
            var timezones = Globalization.GetTimezones();
            return timezones.Select(t => new SelectListItem { Text = t.DisplayName, Value = t.Id }).ToList();
        }

        // todo: remove this and put the data in the result
        private List<SelectListItem> GetCurrencyLayoutSelectModel()
        {
            var layouts = Globalization.GetCurrencyLayouts();
            var items = new List<SelectListItem>();
            if (layouts != null)
            {
                items.AddRange(layouts.Select(l => new SelectListItem { Text = l, Value = l }));
            }
            return items;
        }
    }
}