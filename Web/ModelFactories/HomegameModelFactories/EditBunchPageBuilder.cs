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
    public class EditBunchPageBuilder : IEditBunchPageBuilder
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IBunchContextInteractor _contextInteractor;

        public EditBunchPageBuilder(
            IBunchRepository bunchRepository,
            IBunchContextInteractor contextInteractor)
        {
            _bunchRepository = bunchRepository;
            _contextInteractor = contextInteractor;
        }

        public BunchEditPageModel Build(string slug, BunchEditPostModel postModel)
        {
            var bunch = _bunchRepository.GetBySlug(slug);
            var currency = bunch.Currency;

            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));

            var model = new BunchEditPageModel(contextResult)
            {
                CancelUrl = new BunchDetailsUrl(bunch.Slug),
                Heading = string.Format("{0} Settings", bunch.DisplayName),
                Description = bunch.Description,
                HouseRules = bunch.HouseRules,
                DefaultBuyin = bunch.DefaultBuyin,
                TimeZone = bunch.Timezone.Id,
                TimezoneSelectItems = GetTimezoneSelectModel(),
                CurrencySymbol = currency.Symbol,
                CurrencyLayout = bunch.Currency.Layout,
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