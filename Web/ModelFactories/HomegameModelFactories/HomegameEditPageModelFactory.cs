using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Application.Services;
using Application.Urls;
using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Edit;
using Web.Models.UrlModels;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class HomegameEditPageModelFactory : IHomegameEditPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IGlobalization _globalization;

        public HomegameEditPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IGlobalization globalization)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _globalization = globalization;
        }

        private HomegameEditPageModel Create(Homegame homegame)
        {
            var currency = homegame.Currency;

            return new HomegameEditPageModel
                {
                    BrowserTitle = "Edit Homegame",
		            PageProperties = _pagePropertiesFactory.Create(homegame),
			        CancelUrl = new HomegameDetailsUrl(homegame.Slug),
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

        public HomegameEditPageModel Create(Homegame homegame, HomegameEditPostModel postModel)
        {
            var model = Create(homegame);
            if (postModel != null)
            {
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