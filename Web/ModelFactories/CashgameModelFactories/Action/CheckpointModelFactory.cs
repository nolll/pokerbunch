using Core.Classes;
using Core.Classes.Checkpoints;
using Core.Services;
using Infrastructure.System;
using Web.Models.CashgameModels.Action;

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
                    Description = CheckpointTypeName.GetName(checkpoint.Type),
                    Stack = _globalization.FormatCurrency(homegame.Currency, checkpoint.Stack),
                    Timestamp = _globalization.FormatTime(checkpoint.Timestamp),
                    ShowLink = role >= Role.Manager,
                    EditUrl = _urlProvider.GetCashgameCheckpointDeleteUrl(homegame, cashgame, player, checkpoint)
                };
        }
    }
}