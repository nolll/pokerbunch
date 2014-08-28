using System;
using Application.Urls;
using Core.Entities;
using Core.Entities.Checkpoints;

namespace Application.UseCases.Actions
{
    public class CheckpointItem
    {
        public DateTime Time { get; private set; }
        public Url EditUrl { get; private set; }
        public string Type { get; private set; }
        public Money Stack { get; private set; }
        public bool CanEdit { get; private set; }

        public CheckpointItem(Bunch bunch, Cashgame cashgame, Player player, bool isManager, Checkpoint checkpoint)
        {
            Type = checkpoint.Description;
            Stack = new Money(checkpoint.Stack, bunch.Currency);
            Time = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, bunch.Timezone);
            CanEdit = isManager;
            EditUrl = new EditCheckpointUrl(bunch.Slug, cashgame.DateString, player.Id, checkpoint.Id);
        }
    }
}