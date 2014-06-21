﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Application.Services;
using Application.Urls;
using Application.UseCases.BunchContext;
using Core.Entities;
using Core.Repositories;
using Web.Models.CashgameModels.Edit;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories.Edit
{
    public class EditCashgamePageBuilder : IEditCashgamePageBuilder
    {
        private readonly IGlobalization _globalization;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IBunchContextInteractor _contextInteractor;

        public EditCashgamePageBuilder(
            IGlobalization globalization,
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            IBunchContextInteractor contextInteractor)
        {
            _globalization = globalization;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _contextInteractor = contextInteractor;
        }

        public EditCashgamePageModel Build(string slug, string dateStr, CashgameEditPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            var locations = _cashgameRepository.GetLocations(homegame);
            
            var model = Build(slug, cashgame, locations);
            if (postModel != null)
            {
                model.TypedLocation = postModel.TypedLocation;
                model.SelectedLocation = postModel.SelectedLocation;
            }
            return model;
        }

        private EditCashgamePageModel Build(string slug, Cashgame cashgame, IEnumerable<string> locations)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));

            return new EditCashgamePageModel
                {
                    BrowserTitle = "Edit Cashgame",
                    PageProperties = new PageProperties(contextResult),
                    IsoDate = cashgame.StartTime.HasValue ? _globalization.FormatIsoDate(cashgame.StartTime.Value) : null,
                    CancelUrl = new CashgameDetailsUrl(slug, cashgame.DateString),
                    DeleteUrl = new DeleteCashgameUrl(slug, cashgame.DateString),
                    EnableDelete = cashgame.Status != GameStatus.Published,
                    TypedLocation = cashgame.Location,
                    SelectedLocation = cashgame.Location,
                    Locations = locations.Select(l => new SelectListItem { Text = l, Value = l })
                };
        }
    }
}