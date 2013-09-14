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
                    TimezoneSelectModel = GetTimezoneSelectModel(),
                    CurrencyLayoutSelectModel = GetCurrencyLayoutSelectModel()
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
            var timezones = Globalization.GetTimezones();
            return timezones.Select(
                timezone => new SelectListItem
                {
                    Text = timezone.DisplayName,
                    Value = timezone.Id
                }).ToList();
        }

        private List<SelectListItem> GetCurrencyLayoutSelectModel()
        {
            var layouts = Globalization.GetCurrencyLayouts();
            var items = new List<SelectListItem>();
            if (layouts != null)
            {
                items.AddRange(layouts.Select(l => new SelectListItem{Text = l, Value = l}));
            }
            return items;
        }
    }
}