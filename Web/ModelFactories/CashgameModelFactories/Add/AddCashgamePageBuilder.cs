using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Application.Exceptions;
using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Add;

namespace Web.ModelFactories.CashgameModelFactories.Add
{
    public class AddCashgamePageBuilder : IAddCashgamePageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public AddCashgamePageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
        }

        public AddCashgamePageModel Build(string slug, AddCashgamePostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            if (runningGame != null)
            {
                throw new CashgameRunningException();
            }
            var locations = _cashgameRepository.GetLocations(homegame);
            
            var model = Build(homegame, locations);
            if (postModel != null)
            {
                model.TypedLocation = postModel.TypedLocation;
                model.SelectedLocation = postModel.SelectedLocation;
            }
            return model;
        }

        private AddCashgamePageModel Build(Homegame homegame, IEnumerable<string> locations)
        {
            return new AddCashgamePageModel
                {
                    BrowserTitle = "New Cashgame",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    Locations = GetLocationListItems(locations)
                };
        }

        private IEnumerable<SelectListItem> GetLocationListItems(IEnumerable<string> locations)
        {
            var listItems = locations.Select(l => new SelectListItem {Text = l, Value = l});
            var firstItem = new SelectListItem{Text = "Select Location", Value = ""};
            return new List<SelectListItem> {firstItem}.Concat(listItems);
        } 
    }
}