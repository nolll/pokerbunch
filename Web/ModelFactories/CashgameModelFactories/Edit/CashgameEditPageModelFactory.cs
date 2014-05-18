using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Application.Services;
using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Edit;

namespace Web.ModelFactories.CashgameModelFactories.Edit
{
    public class CashgameEditPageModelFactory : ICashgameEditPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUrlProvider _urlProvider;
        private readonly IGlobalization _globalization;

        public CashgameEditPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider,
            IGlobalization globalization)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
            _globalization = globalization;
        }

        private CashgameEditPageModel Create(Homegame homegame, Cashgame cashgame, IEnumerable<string> locations)
        {
            return new CashgameEditPageModel
                {
                    BrowserTitle = "Edit Cashgame",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    IsoDate = cashgame.StartTime.HasValue ? _globalization.FormatIsoDate(cashgame.StartTime.Value) : null,
			        CancelUrl = _urlProvider.GetCashgameDetailsUrl(homegame.Slug, cashgame.DateString),
			        DeleteUrl = _urlProvider.GetCashgameDeleteUrl(homegame.Slug, cashgame.DateString),
			        EnableDelete = cashgame.Status != GameStatus.Published,
                    TypedLocation = cashgame.Location,
                    SelectedLocation = cashgame.Location,
                    Locations = locations.Select(l => new SelectListItem { Text = l, Value = l })
                };
        }

        public CashgameEditPageModel Create(Homegame homegame, Cashgame cashgame, IEnumerable<string> locations, CashgameEditPostModel postModel)
        {
            var model = Create(homegame, cashgame, locations);
            if (postModel != null)
            {
                model.TypedLocation = postModel.TypedLocation;
                model.SelectedLocation = postModel.SelectedLocation;
            }
            return model;
        }
    }
}