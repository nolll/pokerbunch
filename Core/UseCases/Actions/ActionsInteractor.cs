using System;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases.Actions
{
    public class ActionsInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAuth _auth;

        public ActionsInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository, IAuth auth)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _auth = auth;
        }

        public ActionsOutput Execute(ActionsInput input)
        {
            var bunch = _bunchRepository.GetBySlug(input.Slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch.Id, input.DateStr);
            var player = _playerRepository.GetById(input.PlayerId);
            var playerResult = cashgame.GetResult(player.Id);
            var isManager = _auth.IsInRole(bunch.Slug, Role.Manager);

            var date = cashgame.StartTime.HasValue ? cashgame.StartTime.Value : DateTime.MinValue;
            var playerName = player.DisplayName;
            var checkpointItems = playerResult.Checkpoints.Select(o => CreateCheckpointItem(bunch, cashgame, player, isManager, o)).ToList();

            return new ActionsOutput(date, playerName, checkpointItems);
        }

        private static CheckpointItem CreateCheckpointItem(Bunch bunch, Cashgame cashgame, Player player, bool isManager, Checkpoint checkpoint)
        {
            var type = checkpoint.Description;
            var displayAmount = new Money(GetDisplayAmount(checkpoint), bunch.Currency);
            var time = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, bunch.Timezone);
            var canEdit = isManager;
            var editUrl = new EditCheckpointUrl(bunch.Slug, cashgame.DateString, player.Id, checkpoint.Id);

            return new CheckpointItem(time, editUrl, type, displayAmount, canEdit);
        }

        private static int GetDisplayAmount(Checkpoint checkpoint)
        {
            if (checkpoint.Type == CheckpointType.Buyin)
                return checkpoint.Amount;
            return checkpoint.Stack;
        }
    }
}