using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Application.UseCases.AddBunchForm;
using Application.UseCases.AppContext;
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

        public AddBunchPageModel(AppContextResult contextResult, AddBunchFormResult bunchFormResult, AddBunchPostModel postModel)
            : base("Create Homegame", contextResult)
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
    }
}