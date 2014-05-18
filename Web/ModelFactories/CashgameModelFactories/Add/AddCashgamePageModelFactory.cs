using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Add;

namespace Web.ModelFactories.CashgameModelFactories.Add
{
    public class AddCashgamePageModelFactory : IAddCashgamePageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public AddCashgamePageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        private AddCashgamePageModel Create(Homegame homegame, IEnumerable<string> locations)
        {
            return new AddCashgamePageModel
                {
                    BrowserTitle = "New Cashgame",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    Locations = GetLocationListItems(locations)
                };
        }

        public AddCashgamePageModel Create(Homegame homegame, IEnumerable<string> locations, AddCashgamePostModel postModel)
        {
            var model = Create(homegame, locations);
            if (postModel != null)
            {
                model.TypedLocation = postModel.TypedLocation;
                model.SelectedLocation = postModel.SelectedLocation;
            }
            return model;
        }

        private IEnumerable<SelectListItem> GetLocationListItems(IEnumerable<string> locations)
        {
            var listItems = locations.Select(l => new SelectListItem {Text = l, Value = l});
            var firstItem = new SelectListItem{Text = "Select Location", Value = ""};
            return new List<SelectListItem> {firstItem}.Concat(listItems);
        } 
    }
}