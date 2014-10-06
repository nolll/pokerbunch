﻿using System.Linq;
using System.Web.Mvc;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using Core.UseCases.BunchContext;
using Plumbing;
using Web.Models.CashgameModels.Edit;

namespace Web.ModelFactories.CashgameModelFactories.Edit
{
    public class EditCashgamePageBuilder : IEditCashgamePageBuilder
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public EditCashgamePageBuilder(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public EditCashgamePageModel Build(string slug, string dateStr, CashgameEditPostModel postModel)
        {
            var homegame = _bunchRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            var locations = _cashgameRepository.GetLocations(homegame);

            var contextResult = UseCaseContainer.Instance.BunchContext(new BunchContextRequest(slug));

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