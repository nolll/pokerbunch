using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Application.Services;
using Application.Urls;
using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Edit;

namespace Web.ModelFactories.CashgameModelFactories.Edit
{
    public class EditCashgamePageBuilder : IEditCashgamePageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IGlobalization _globalization;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public EditCashgamePageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IGlobalization globalization,
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _globalization = globalization;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
        }

        public EditCashgamePageModel Build(string slug, string dateStr, CashgameEditPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            var locations = _cashgameRepository.GetLocations(homegame);
            
            var model = Build(homegame, cashgame, locations);
            if (postModel != null)
            {
                model.TypedLocation = postModel.TypedLocation;
                model.SelectedLocation = postModel.SelectedLocation;
            }
            return model;
        }

        private EditCashgamePageModel Build(Homegame homegame, Cashgame cashgame, IEnumerable<string> locations)
        {
            return new EditCashgamePageModel
                {
                    BrowserTitle = "Edit Cashgame",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    IsoDate = cashgame.StartTime.HasValue ? _globalization.FormatIsoDate(cashgame.StartTime.Value) : null,
                    CancelUrl = new CashgameDetailsUrl(homegame.Slug, cashgame.DateString),
                    DeleteUrl = new DeleteCashgameUrl(homegame.Slug, cashgame.DateString),
                    EnableDelete = cashgame.Status != GameStatus.Published,
                    TypedLocation = cashgame.Location,
                    SelectedLocation = cashgame.Location,
                    Locations = locations.Select(l => new SelectListItem { Text = l, Value = l })
                };
        }
    }
}