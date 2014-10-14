using System;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Factories;
using Core.Repositories;
using Core.Services.Interfaces;
using Web.Models.CashgameModels.Cashout;

namespace Web.Commands.CashgameCommands
{
    public class CashoutCommand : Command
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly ITimeProvider _timeProvider;
        private readonly Cashgame _cashgame;
        private readonly Player _player;
        private readonly CashgameResult _result;
        private readonly CashoutPostModel _model;

        public CashoutCommand(
            ICheckpointRepository checkpointRepository,
            ITimeProvider timeProvider,
            Cashgame cashgame,
            Player player,
            CashgameResult result,
            CashoutPostModel model)
        {
            _checkpointRepository = checkpointRepository;
            _timeProvider = timeProvider;
            _cashgame = cashgame;
            _player = player;
            _result = result;
            _model = model;
        }

        public override bool Execute()
        {
            if (!IsValid(_model)) return false;
            var postedCheckpoint = CreateCheckpoint(_model, _result.CashoutCheckpoint);
            if (_result.CashoutCheckpoint != null)
                _checkpointRepository.UpdateCheckpoint(_cashgame, postedCheckpoint);
            else
                _checkpointRepository.AddCheckpoint(_cashgame, _player, postedCheckpoint);
            return true;
        }

        private Checkpoint CreateCheckpoint(CashoutPostModel postModel, Checkpoint existingCashoutCheckpoint)
        {
            return CheckpointFactory.Create(
                _timeProvider.UtcNow(),
                CheckpointType.Cashout,
                postModel.StackAmount.HasValue ? postModel.StackAmount.Value : 0,
                existingCashoutCheckpoint != null ? existingCashoutCheckpoint.Id : 0);
        }

        // todo: display sharing options
        private string GetMessage(int amount)
        {
            var formattedAmount = Math.Abs(amount) + " kr";
            var wonOrLost = amount < 0 ? "lost" : "won";
            return string.Format("I just {0} {1} playing poker. #pokerbunch", wonOrLost, formattedAmount);
        }
    }
}