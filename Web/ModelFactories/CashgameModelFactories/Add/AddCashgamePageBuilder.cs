using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Application.Exceptions;
using Application.UseCases.BunchContext;
using Core.Repositories;
using Web.Models.CashgameModels.Add;

namespace Web.ModelFactories.CashgameModelFactories.Add
{
    public class AddCashgamePageBuilder : IAddCashgamePageBuilder
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IBunchContextInteractor _contextInteractor;

        public AddCashgamePageBuilder(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            IBunchContextInteractor contextInteractor)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _contextInteractor = contextInteractor;
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
            
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));

            return new AddCashgamePageModel(contextResult, postModel)
            {
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