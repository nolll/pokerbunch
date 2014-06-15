using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Application.Urls;
using Application.UseCases.Actions;
using Application.UseCases.CashgameContext;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Web.Models.CashgameModels.Action;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories.Action
{
    public class ActionPageBuilder : IActionPageBuilder
    {
        private readonly ICheckpointModelFactory _checkpointModelFactory;
        private readonly IGlobalization _globalization;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAuth _auth;
        private readonly ICashgameContextInteractor _cashgameContextInteractor;
        private readonly IActionsInteractor _actionsInteractor;

        public ActionPageBuilder(
            ICheckpointModelFactory checkpointModelFactory,
            IGlobalization globalization,
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            IAuth auth,
            ICashgameContextInteractor cashgameContextInteractor,
            IActionsInteractor actionsInteractor)
        {
            _checkpointModelFactory = checkpointModelFactory;
            _globalization = globalization;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _auth = auth;
            _cashgameContextInteractor = cashgameContextInteractor;
            _actionsInteractor = actionsInteractor;
        }

        public ActionPageModel Build(string slug, string dateStr, int playerId)
        {
            var cashgameContextResult = _cashgameContextInteractor.Execute(new CashgameContextRequest{Slug = slug});
            var actionsResult = _actionsInteractor.Execute(new ActionsRequest(slug, dateStr, playerId));

            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            var player = _playerRepository.GetById(playerId);
            var result = cashgame.GetResult(player.Id);
            var role = _auth.GetRole(slug);

            var heading = string.Format("Cashgame {0}, {1}", actionsResult.Date, actionsResult.PlayerName);
            
            return new ActionPageModel
                {
                    BrowserTitle = "Player Actions",
                    PageProperties = new PageProperties(cashgameContextResult),
                    Heading = heading,
                    Checkpoints = GetCheckpointModels(homegame, cashgame, result, player, role),
                    ChartDataUrl = actionsResult.ChartDataUrl
                };
        }

        private List<CheckpointModel> GetCheckpointModels(Homegame homegame, Cashgame cashgame, CashgameResult result, Player player, Role role)
        {
            var checkpoints = GetCheckpoints(result);
            return checkpoints.Select(checkpoint => _checkpointModelFactory.Create(homegame, cashgame, player, checkpoint, role)).ToList();
        }

        private IEnumerable<Checkpoint> GetCheckpoints(CashgameResult result)
        {
            return PlayerIsInGame(result) ? result.Checkpoints : new List<Checkpoint>();
        }

        private bool PlayerIsInGame(CashgameResult result)
        {
            return result != null;
        }
    }
}