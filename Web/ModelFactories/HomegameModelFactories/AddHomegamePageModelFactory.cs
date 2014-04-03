using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Application.Services;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Add;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class AddHomegamePageModelFactory : IAddHomegamePageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IGlobalization _globalization;

        public AddHomegamePageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IGlobalization globalization)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _globalization = globalization;
        }

        private AddHomegamePageModel Create()
        {
            return new AddHomegamePageModel
                {
                    BrowserTitle = "Create Homegame",
                    PageProperties = _pagePropertiesFactory.Create(),
                    TimezoneSelectItems = GetTimezoneSelectModel(),
                    CurrencyLayoutSelectItems = GetCurrencyLayoutSelectModel()
                };
        }

        public AddHomegamePageModel Create(AddHomegamePostModel postModel)
        {
            var model = Create();
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