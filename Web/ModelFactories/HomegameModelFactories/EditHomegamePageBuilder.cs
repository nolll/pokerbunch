using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Application.Services;
using Application.Urls;
using Application.UseCases.AppContext;
using Core.Repositories;
using Web.Models.HomegameModels.Edit;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class EditHomegamePageBuilder : IEditHomegamePageBuilder
    {
        private readonly IGlobalization _globalization;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IAppContextInteractor _contextInteractor;

        public EditHomegamePageBuilder(
            IGlobalization globalization,
            IHomegameRepository homegameRepository,
            IAppContextInteractor contextInteractor)
        {
            _globalization = globalization;
            _homegameRepository = homegameRepository;
            _contextInteractor = contextInteractor;
        }

        private HomegameEditPageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var currency = homegame.Currency;

            var contextResult = _contextInteractor.Execute();

            return new HomegameEditPageModel
                {
                    BrowserTitle = "Edit Homegame",
		            PageProperties = new PageProperties(contextResult),
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

        public HomegameEditPageModel Build(string slug, HomegameEditPostModel postModel)
        {
            var model = Build(slug);
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