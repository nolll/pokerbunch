using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Application.Services;
using Application.UseCases.AppContext;
using Web.Models.HomegameModels.Add;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class AddHomegamePageBuilder : IAddHomegamePageBuilder
    {
        private readonly IGlobalization _globalization;
        private readonly IAppContextInteractor _contextInteractor;

        public AddHomegamePageBuilder(
            IGlobalization globalization,
            IAppContextInteractor contextInteractor)
        {
            _globalization = globalization;
            _contextInteractor = contextInteractor;
        }

        public AddHomegamePageModel Build(AddHomegamePostModel postModel)
        {
            var model = Build();
            if (postModel != null)
            {
                model.DisplayName = postModel.DisplayName;
                model.Description = postModel.Description;
                model.TimeZone = postModel.TimeZone;
                model.CurrencySymbol = postModel.CurrencySymbol;
                model.CurrencyLayout = postModel.CurrencyLayout;
            }
            return model;
        }

        private AddHomegamePageModel Build()
        {
            var contextResult = _contextInteractor.Execute();

            return new AddHomegamePageModel
                {
                    BrowserTitle = "Create Homegame",
                    PageProperties = new PageProperties(contextResult),
                    TimezoneSelectItems = GetTimezoneSelectModel(),
                    CurrencyLayoutSelectItems = GetCurrencyLayoutSelectModel()
                };
        }

        private List<SelectListItem> GetTimezoneSelectModel()
        {
            var timezones = _globalization.GetTimezones();
            return timezones.Select(t => new SelectListItem{ Text = t.DisplayName, Value = t.Id }).ToList();
        }

        private List<SelectListItem> GetCurrencyLayoutSelectModel()
        {
            var layouts = _globalization.GetCurrencyLayouts();
            var items = new List<SelectListItem>();
            if (layouts != null)
            {
                items.AddRange(layouts.Select(l => new SelectListItem{ Text = l, Value = l }));
            }
            return items;
        }
    }
}