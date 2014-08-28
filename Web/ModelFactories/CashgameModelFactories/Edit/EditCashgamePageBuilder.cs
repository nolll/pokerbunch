using System.Linq;
using System.Web.Mvc;
using Application.Services;
using Application.Urls;
using Application.UseCases.BunchContext;
using Core.Repositories;
using Web.Models.CashgameModels.Edit;

namespace Web.ModelFactories.CashgameModelFactories.Edit
{
    public class EditCashgamePageBuilder : IEditCashgamePageBuilder
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IBunchContextInteractor _contextInteractor;

        public EditCashgamePageBuilder(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IBunchContextInteractor contextInteractor)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _contextInteractor = contextInteractor;
        }

        public EditCashgamePageModel Build(string slug, string dateStr, CashgameEditPostModel postModel)
        {
            var homegame = _bunchRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            var locations = _cashgameRepository.GetLocations(homegame);

            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));

            var model = new EditCashgamePageModel(contextResult)
            {
                IsoDate = cashgame.StartTime.HasValue ? Globalization.FormatIsoDate(cashgame.StartTime.Value) : null,
                CancelUrl = new CashgameDetailsUrl(slug, cashgame.DateString),
                DeleteUrl = new DeleteCashgameUrl(slug, cashgame.DateString),
                TypedLocation = cashgame.Location,
                SelectedLocation = cashgame.Location,
                Locations = locations.Select(l => new SelectListItem { Text = l, Value = l })
            };

            if (postModel != null)
            {
                model.TypedLocation = postModel.TypedLocation;
                model.SelectedLocation = postModel.SelectedLocation;
            }

            return model;
        }
    }
}