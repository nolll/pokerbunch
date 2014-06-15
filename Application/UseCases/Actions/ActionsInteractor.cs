using System;
using System.Collections.Generic;
using Application.Services;
using Application.Urls;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using System.Linq;

namespace Application.UseCases.Actions
{
    public class ActionsInteractor : IActionsInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGlobalization _globalization;
        private readonly IAuth _auth;

        public ActionsInteractor(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            IGlobalization globalization,
            IAuth auth)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _globalization = globalization;
            _auth = auth;
        }

        public ActionsResult Execute(ActionsRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, request.DateStr);
            var player = _playerRepository.GetById(request.PlayerId);
            var playerResult = cashgame.GetResult(player.Id);

            var date = cashgame.StartTime.HasValue ? _globalization.FormatShortDate(cashgame.StartTime.Value, true) : string.Empty;
            var chartDataUrl = new CashgameActionChartJsonUrl(homegame.Slug, cashgame.DateString, player.Id);
            var checkpointItems = GetCheckpointItems(homegame, cashgame, player, playerResult);

            return new ActionsResult
                {
                    Date = date,
                    PlayerName = player.DisplayName,
                    ChartDataUrl = chartDataUrl,
                    CheckpointItems = checkpointItems
                };
        }

        private IList<CheckpointItem> GetCheckpointItems(Homegame homegame, Cashgame cashgame, Player player, CashgameResult result)
        {
            return result.Checkpoints.Select(o => GetCheckpointItem(homegame, cashgame, player, o)).ToList();
        }

        private CheckpointItem GetCheckpointItem(Homegame homegame, Cashgame cashgame, Player player, Checkpoint checkpoint)
        {
            var role = _auth.GetRole(homegame.Slug);

            return new CheckpointItem
                {
                    Type = checkpoint.Description,
                    Stack = new Money(checkpoint.Stack, homegame.Currency),
                    Time = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, homegame.Timezone),
                    CanEdit = role >= Role.Manager,
                    EditUrl = new EditCheckpointUrl(homegame.Slug, cashgame.DateString, player.Id, checkpoint.Id)
                };
        }
    }

    public class CheckpointItem
    {
        public DateTime Time { get; set; }
        public Url EditUrl { get; set; }
        public string Type { get; set; }
        public Money Stack { get; set; }
        public bool CanEdit { get; set; }
    }
}