using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Edit;

namespace Web.ModelFactories.CashgameModelFactories.Edit
{
    public class CashgameEditPageModelFactory : ICashgameEditPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUrlProvider _urlProvider;

        public CashgameEditPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
        }

        public CashgameEditPageModel Create(User user, Homegame homegame, Cashgame cashgame, IList<string> locations, IList<int> years, Cashgame runningGame)
        {
            return new CashgameEditPageModel
                {
                    BrowserTitle = "Edit Cashgame",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
                    IsoDate = cashgame.StartTime.HasValue ? StaticGlobalization.FormatIsoDate(cashgame.StartTime.Value) : null,
			        CancelUrl = _urlProvider.GetCashgameDetailsUrl(homegame, cashgame),
			        DeleteUrl = _urlProvider.GetCashgameDeleteUrl(homegame, cashgame),
			        EnableDelete = cashgame.Status != GameStatus.Published,
                    TypedLocation = cashgame.Location,
                    SelectedLocation = cashgame.Location,
                    Locations = locations.Select(l => new SelectListItem { Text = l, Value = l })
                };
        }

        public CashgameEditPageModel Create(User user, Homegame homegame, Cashgame cashgame, IList<string> locations, IList<int> years, Cashgame runningGame, CashgameEditPostModel postModel)
        {
            var model = Create(user, homegame, cashgame, locations, years, runningGame);

            return model;
        }
    }
}