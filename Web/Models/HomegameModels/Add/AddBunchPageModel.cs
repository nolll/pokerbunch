using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add
{
    public class AddBunchPageModel : AppPageModel
    {
        public List<SelectListItem> CurrencyLayoutSelectItems { get; private set; }
        public List<SelectListItem> TimezoneSelectItems { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        public string CurrencySymbol { get; private set; }
        public string CurrencyLayout { get; private set; }
        public string TimeZone { get; private set; }

        public AddBunchPageModel(CoreContext.Result contextResult, AddBunchForm.Result bunchFormResult, AddBunchPostModel postModel)
            : base(contextResult)
        {
            CurrencyLayoutSelectItems = bunchFormResult.CurrencyLayouts.Select(o => new SelectListItem{ Text = o, Value = o }).ToList();
            TimezoneSelectItems = bunchFormResult.TimeZones.Select(o => new SelectListItem{ Text = o.Name, Value = o.Id }).ToList();
            if (postModel == null) return;
            DisplayName = postModel.DisplayName;
            Description = postModel.Description;
            TimeZone = postModel.TimeZone;
            CurrencySymbol = postModel.CurrencySymbol;
            CurrencyLayout = postModel.CurrencyLayout;
        }

        public override string BrowserTitle => "Create Bunch";
    }
}