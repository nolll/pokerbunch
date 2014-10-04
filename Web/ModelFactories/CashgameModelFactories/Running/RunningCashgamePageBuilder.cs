using System;
using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Application.Urls;
using Application.UseCases.BunchContext;
using Application.UseCases.RunningCashgame;
using Core.Entities;
using Core.Repositories;
using Plumbing;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public class RunningCashgamePageBuilder : IRunningCashgamePageBuilder
    {
        private readonly IRunningCashgameTableModelFactory _runningCashgameTableModelFactory;
        private readonly IAuth _auth;
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ITimeProvider _timeProvider;
        private readonly IPlayerRepository _playerRepository;

        public RunningCashgamePageBuilder(
            IRunningCashgameTableModelFactory runningCashgameTableModelFactory,
            IAuth auth,
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            ITimeProvider timeProvider,
            IPlayerRepository playerRepository)
        {
            _runningCashgameTableModelFactory = runningCashgameTableModelFactory;
            _auth = auth;
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _timeProvider = timeProvider;
            _playerRepository = playerRepository;
        }

        public RunningCashgamePageModel Build(string slug)
        {
            var homegame = _bunchRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetRunning(homegame);
            var isManager = _auth.IsInRole(slug, Role.Manager);
            var now = _timeProvider.GetTime();
            var players = GetPlayers(cashgame);
            
            var contextResult = UseCaseContainer.Instance.BunchContext(new BunchContextRequest(slug));
            var runningCashgameResult = UseCaseContainer.Instance.RunningCashgame(new RunningCashgameRequest(slug));

            return new RunningCashgamePageModel(contextResult, runningCashgameResult)
                {
                    RunningCashgameTableModel = cashgame.IsStarted ? _runningCashgameTableModelFactory.Create(homegame, cashgame, players, isManager, now) : null,
                };
        }

        private IList<Player> GetPlayers(Cashgame cashgame)
        {
            var ids = GetPlayerIds(cashgame);
            return _playerRepository.GetList(ids);
        }

        private IList<int> GetPlayerIds(Cashgame cashgame)
        {
            return cashgame.Results.Select(o => o.PlayerId).ToList();
        }
    }
}