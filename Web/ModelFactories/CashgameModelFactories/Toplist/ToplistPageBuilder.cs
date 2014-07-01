using System;
using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameTopList;
using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public class ToplistPageBuilder : IToplistPageBuilder
    {
        private readonly ITopListInteractor _topListInteractor;
        private readonly ICashgameContextInteractor _contextInteractor;

        public ToplistPageBuilder(
            ITopListInteractor topListInteractor,
            ICashgameContextInteractor contextInteractor)
        {
            _topListInteractor = topListInteractor;
            _contextInteractor = contextInteractor;
        }

        public CashgameToplistPageModel Build(string slug, string sortOrderParam, int? year)
        {
            var contextResult = _contextInteractor.Execute(new CashgameContextRequest(slug, year));
            var topListResult = _topListInteractor.Execute(new TopListRequest(slug, sortOrderParam, year));

            return new CashgameToplistPageModel(contextResult, topListResult);
        }
    }
}