using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Classes;
using Infrastructure.System;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Edit;
using Web.Models.UrlModels;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class HomegameEditPageModelFactory : IHomegameEditPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public HomegameEditPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public HomegameEditPageModel Create(User user, Homegame homegame, Cashgame runningGame)
        {
            var currency = homegame.Currency;

            return new HomegameEditPageModel
                {
                    BrowserTitle = "Edit Homegame",
		            PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
			        CancelUrl = new HomegameDetailsUrlModel(homegame),
		            Heading = string.Format("{0} Settings", homegame.DisplayName),
			        Description = homegame.Description,
			        HouseRules = homegame.HouseRules,
			        DefaultBuyin = homegame.DefaultBuyin,
                    TimeZone = homegame.Timezone.Id,
                    TimezoneSelectItems = GetTimezoneSelectModel(),
                    CurrencySymbol = currency.Symbol,
			        CurrencyLayout = homegame.Currency.Layout,
			        CurrencyLayoutSelectItems = GetCurrencyLayoutSelectModel()
			        //CashgamesEnabled = homegame.CashgamesEnabled,
			        //TournamentsEnabled = homegame.TournamentsEnabled,
			        //VideosEnabled = homegame.VideosEnabled
                };
        }

        public HomegameEditPageModel Create(User user, Homegame homegame, Cashgame runningGame, HomegameEditPostModel postModel)
        {
            var model = Create(user, homegame, runningGame);
            model.Description = postModel.Description;
            model.TimeZone = postModel.TimeZone;
            model.CurrencySymbol = postModel.CurrencySymbol;
            model.CurrencyLayout = postModel.CurrencyLayout;
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