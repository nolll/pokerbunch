using System;
using Application.Services;
using Core.Entities;
using Core.Entities.Checkpoints;
using Web.Models.CashgameModels.Action;
using Web.Services;

namespace Web.ModelFactories.CashgameModelFactories.Action
{
    public class CheckpointModelFactory : ICheckpointModelFactory
    {
        private readonly IUrlProvider _urlProvider;
        private readonly IGlobalization _globalization;

        public CheckpointModelFactory(
            IUrlProvider urlProvider,
            IGlobalization globalization)
        {
            _urlProvider = urlProvider;
            _globalization = globalization;
        }

        public CheckpointModel Create(Homegame homegame, Cashgame cashgame, Player player, Checkpoint checkpoint, Role role)
        {
            return new CheckpointModel
                {
                    Description = checkpoint.Description,
                    Stack = _globalization.FormatCurrency(homegame.Currency, checkpoint.Stack),
                    Timestamp = _globalization.FormatTime(TimeZoneInfo.ConvertTime(checkpoint.Timestamp, homegame.Timezone)),
                    ShowLink = role >= Role.Manager,
                    EditUrl = new EditCheckpointUrlModel(homegame.Slug, cashgame.DateString, player.Id, checkpoint.Id)
                };
        }
    }
}