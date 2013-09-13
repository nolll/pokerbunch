using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Classes;
using Infrastructure.System;
using Web.Models.FormModels;
using Web.Models.HomegameModels.Add;
using Web.Models.MiscModels;
using Web.Models.NavigationModels;

namespace Web.Controllers
{
    public class AddHomegamePageModelFactory : IAddHomegamePageModelFactory
    {
        public AddHomegamePageModel Create(User user, Homegame homegame = null)
        {
            var timezone = GetTimeZone(homegame);
            var currency = GetCurrency(homegame);

            return new AddHomegamePageModel
                {
                    UserNavModel = new UserNavigationModel(user),
			        GoogleAnalyticsModel = new GoogleAnalyticsModel(),
                    DisplayName = homegame != null ? homegame.DisplayName : null,
                    Description = homegame != null ? homegame.Description : null,
                    TimeZone = timezone.Id,
                    TimezoneSelectModel = GetTimezoneSelectModel(),
			        CurrencySymbol = currency.Symbol,
                    CurrencyLayout = currency.Layout,
			        CurrencyLayoutSelectModel = GetCurrencyLayoutSelectModel()
                };
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