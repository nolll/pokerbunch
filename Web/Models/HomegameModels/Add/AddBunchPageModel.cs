using System.Collections.Generic;
using System.Linq;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Add
{
    public class AddBunchPageModel : AppPageModel
    {
        public List<SelectListItem> CurrencyLayoutSelectItems { get; }
        public List<SelectListItem> TimezoneSelectItems { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public string CurrencySymbol { get; }
        public string CurrencyLayout { get; }
        public string TimeZone { get; }
        public ErrorListModel Errors { get; }

        public AddBunchPageModel(AppSettings appSettings, CoreContext.Result contextResult, AddBunchForm.Result bunchFormResult, AddBunchPostModel postModel, IEnumerable<string> errors)
            : base(appSettings, contextResult)
        {
            CurrencyLayoutSelectItems = bunchFormResult.CurrencyLayouts.Select(o => new SelectListItem{ Text = o, Value = o }).ToList();
            TimezoneSelectItems = bunchFormResult.TimeZones.Select(o => new SelectListItem{ Text = o.Name, Value = o.Id }).ToList();
            if (postModel == null) return;
            DisplayName = postModel.DisplayName;
            Description = postModel.Description;
            TimeZone = postModel.TimeZone;
            CurrencySymbol = postModel.CurrencySymbol;
            CurrencyLayout = postModel.CurrencyLayout;
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Create Bunch";

        public override View GetView()
        {
            return new View("~/Views/Pages/AddBunch/AddBunch.cshtml");
        }
    }
}