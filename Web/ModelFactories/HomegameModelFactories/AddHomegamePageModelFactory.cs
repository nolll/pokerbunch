using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Classes;
using Infrastructure.System;
using Web.Controllers;
using Web.Models.HomegameModels.Add;
using Web.Models.MiscModels;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class AddHomegamePageModelFactory : IAddHomegamePageModelFactory
    {
        public AddHomegamePageModel Create(User user, Homegame homegame = null)
        {
            var timezone = GetTimeZone(homegame);
            var currency = GetCurrency(homegame);

            var model = new AddHomegamePageModel
                {
                    DisplayName = homegame != null ? homegame.DisplayName : null,
                    Description = homegame != null ? homegame.Description : null,
                    TimeZone = timezone.Id,
			        CurrencySymbol = currency.Symbol,
                    CurrencyLayout = currency.Layout
                };
            return FillModel(user, model);
        }

        public AddHomegamePageModel ReBuild(User user, AddHomegamePageModel model)
        {
            return FillModel(user, model);
        }

        private AddHomegamePageModel FillModel(User user, AddHomegamePageModel model)
        {
            model.UserNavModel = new UserNavigationModel(user);
            model.GoogleAnalyticsModel = new GoogleAnalyticsModel();
            model.TimezoneSelectModel = GetTimezoneSelectModel();
            model.CurrencyLayoutSelectModel = GetCurrencyLayoutSelectModel();
            return model;
        }

        private TimeZoneInfo GetTimeZone(Homegame homegame)
        {
            return homegame != null ? homegame.Timezone : Homegame.DefaultTimezone;
        }

        private CurrencySettings GetCurrency(Homegame homegame)
        {
            return homegame != null ? homegame.Currency : Homegame.DefaultCurrency;
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