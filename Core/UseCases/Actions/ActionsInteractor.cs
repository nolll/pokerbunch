using System;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases.Actions
{
    public static class ActionsInteractor
    {
        public static ActionsOutput Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            IAuth auth,
            ActionsInput input)
        {
            var bunch = bunchRepository.GetBySlug(input.Slug);
            var cashgame = cashgameRepository.GetByDateString(bunch, input.DateStr);
            var player = playerRepository.GetById(input.PlayerId);
            var playerResult = cashgame.GetResult(player.Id);
            var isManager = auth.IsInRole(bunch.Slug, Role.Manager);

            var date = cashgame.StartTime.HasValue ? cashgame.StartTime.Value : DateTime.MinValue;
            var playerName = player.DisplayName;
            var chartDataUrl = new CashgameActionChartJsonUrl(bunch.Slug, cashgame.DateString, player.Id);
            var checkpointItems = playerResult.Checkpoints.Select(o => CreateCheckpointItem(bunch, cashgame, player, isManager, o)).ToList();

            return new ActionsOutput(date, playerName, chartDataUrl, checkpointItems);
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