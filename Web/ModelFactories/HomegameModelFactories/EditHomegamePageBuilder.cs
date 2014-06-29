using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Application.Services;
using Application.Urls;
using Application.UseCases.BunchContext;
using Core.Repositories;
using Web.Models.HomegameModels.Edit;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class EditHomegamePageBuilder : IEditHomegamePageBuilder
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IBunchContextInteractor _contextInteractor;

        public EditHomegamePageBuilder(
            IHomegameRepository homegameRepository,
            IBunchContextInteractor contextInteractor)
        {
            _homegameRepository = homegameRepository;
            _contextInteractor = contextInteractor;
        }

        public HomegameEditPageModel Build(string slug, HomegameEditPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var currency = homegame.Currency;

            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));

            var model = new HomegameEditPageModel(contextResult)
            {
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