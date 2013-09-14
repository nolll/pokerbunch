using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Classes;
using Web.Models.CashgameModels.Add;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class AddCashgamePageModelFactory : IAddCashgamePageModelFactory
    {
        public AddCashgamePageModel Create(User user, Homegame homegame, IEnumerable<string> locations)
        {
            return new AddCashgamePageModel
                {
                    BrowserTitle = "New Cashgame",
                    PageProperties = new PageProperties(user, homegame),
                    Locations = locations.Select(l => new SelectListItem{Text = l, Value = l})
                };
        }

        public AddCashgamePageModel Create(User user, Homegame homegame, IEnumerable<string> locations, AddCashgamePostModel postModel)
        {
            var model = Create(user, homegame, locations);
            model.TypedLocation = postModel.TypedLocation;
            model.SelectedLocation = postModel.SelectedLocation;
            return model;
        }
    }
}