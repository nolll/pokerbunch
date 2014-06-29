using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Application.Services;
using Application.UseCases.AppContext;
using Web.Models.HomegameModels.Add;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class AddHomegamePageBuilder : IAddHomegamePageBuilder
    {
        private readonly IAppContextInteractor _contextInteractor;

        public AddHomegamePageBuilder(IAppContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public AddHomegamePageModel Build(AddHomegamePostModel postModel)
        {
            var contextResult = _contextInteractor.Execute();

            var model = new AddHomegamePageModel(contextResult)
            {
                TimezoneSelectItems = GetTimezoneSelectModel(),
                CurrencyLayoutSelectItems = GetCurrencyLayoutSelectModel()
            };

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

        private List<SelectListItem> GetTimezoneSelectModel()
        {
            var timezones = Globalization.GetTimezones();
            return timezones.Select(t => new SelectListItem{ Text = t.DisplayName, Value = t.Id }).ToList();
        }

        private List<SelectListItem> GetCurrencyLayoutSelectModel()
        {
            var layouts = Globalization.GetCurrencyLayouts();
            var items = new List<SelectListItem>();
            if (layouts != null)
            {
                items.AddRange(layouts.Select(l => new SelectListItem{ Text = l, Value = l }));
            }
            return items;
        }
    }
}