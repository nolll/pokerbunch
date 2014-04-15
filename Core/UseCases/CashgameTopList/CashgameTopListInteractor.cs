using System;
using System.Collections.Generic;
using Core.Repositories;
using Core.Services.Interfaces;

namespace Core.UseCases.CashgameTopList
{
    public class CashgameTopListInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameService _cashgameService;

        public CashgameTopListInteractor(
            IHomegameRepository homegameRepository,
            ICashgameService cashgameService)
        {
            _homegameRepository = homegameRepository;
            _cashgameService = cashgameService;
        }

        public CashgameTopListResult Execute(CashgameTopListRequest request)
        {
            if(request.Slug == null)
                throw new ArgumentException("No slug provided");

            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var suite = _cashgameService.GetSuite(homegame);
            
            var item = new TopListItem {Name = ""};
            var items = new List<TopListItem> {item};
            
            return new CashgameTopListResult
                {
                    Items = items
                };
        }
    }
}