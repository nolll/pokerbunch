using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Classes;
using Infrastructure.System;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Add;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class AddHomegamePageModelFactory : IAddHomegamePageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public AddHomegamePageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public AddHomegamePageModel Create(User user)
        {
            return new AddHomegamePageModel
                {
                    BrowserTitle = "Create Homegame",
                    PageProperties = _pagePropertiesFactory.Create(user),
                    TimezoneSelectItems = GetTimezoneSelectModel(),
                    CurrencyLayoutSelectItems = GetCurrencyLayoutSelectModel()
                };
        }

        public AddHomegamePageModel Create(User user, AddHomegamePostModel postModel)
        {
            var model = Create(user);
            model.DisplayName = postModel.DisplayName;
            model.Description = postModel.Description;
            model.TimeZone = postModel.TimeZone;
            model.CurrencySymbol = postModel.CurrencySymbol;
            model.CurrencyLayout = postModel.CurrencyLayout;
            return model;
        }

        private List<SelectListItem> GetTimezoneSelectModel()
        {
            var timezones = StaticGlobalization.GetTimezones();
            return timezones.Select(t => new SelectListItem{ Text = t.DisplayName, Value = t.Id }).ToList();
        }

        private List<SelectListItem> GetCurrencyLayoutSelectModel()
        {
            var layouts = StaticGlobalization.GetCurrencyLayouts();
            var items = new List<SelectListItem>();
            if (layouts != null)
            {
                items.AddRange(layouts.Select(l => new SelectListItem{ Text = l, Value = l }));
            }
            return items;
        }
    }
}