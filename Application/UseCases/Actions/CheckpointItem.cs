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

        public CheckpointItem(Homegame homegame, Cashgame cashgame, Player player, Role role, Checkpoint checkpoint)
        {
            Type = checkpoint.Description;
            Stack = new Money(checkpoint.Stack, homegame.Currency);
            Time = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, homegame.Timezone);
            CanEdit = role >= Role.Manager;
            EditUrl = new EditCheckpointUrl(homegame.Slug, cashgame.DateString, player.Id, checkpoint.Id);
        }
    }
}