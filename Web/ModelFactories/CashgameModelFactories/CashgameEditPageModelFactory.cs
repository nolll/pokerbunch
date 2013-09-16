using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Classes;
using Infrastructure.System;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Edit;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class CashgameEditPageModelFactory : ICashgameEditPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public CashgameEditPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public CashgameEditPageModel Create(User user, Homegame homegame, Cashgame cashgame, List<string> locations, List<int> years, Cashgame runningGame)
        {
            return new CashgameEditPageModel
                {
                    BrowserTitle = "Edit Cashgame",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
                    IsoDate = cashgame.StartTime.HasValue ? Globalization.FormatIsoDate(cashgame.StartTime.Value) : null,
			        CancelUrl = new CashgameDetailsUrlModel(homegame, cashgame),
			        DeleteUrl = new CashgameDeleteUrlModel(homegame, cashgame),
			        EnableDelete = cashgame.Status != GameStatus.Published,
                    TypedLocation = cashgame.Location,
                    SelectedLocation = cashgame.Location,
                    Locations = locations.Select(l => new SelectListItem { Text = l, Value = l })
                };
        }

        public CashgameEditPageModel Create(User user, Homegame homegame, Cashgame cashgame, List<string> locations, List<int> years, Cashgame runningGame, CashgameEditPostModel postModel)
        {
            var model = Create(user, homegame, cashgame, locations, years, runningGame);

            return model;
        }
    }
}