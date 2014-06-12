using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Application.Services;
using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Edit;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories.Edit
{
    public class CashgameEditPageModelFactory : ICashgameEditPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IGlobalization _globalization;

        public CashgameEditPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IGlobalization globalization)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _globalization = globalization;
        }

        private CashgameEditPageModel Create(Homegame homegame, Cashgame cashgame, IEnumerable<string> locations)
        {
            return new CashgameEditPageModel
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