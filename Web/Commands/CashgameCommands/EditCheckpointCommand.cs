using System;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Factories;
using Core.Repositories;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.Commands.CashgameCommands
{
    public class EditCheckpointCommand : Command
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly Cashgame _cashgame;
        private readonly EditCheckpointPostModel _postModel;
        private readonly Checkpoint _existingCheckpoint;
        private readonly TimeZoneInfo _timeZone;

        public EditCheckpointCommand(
            ICheckpointRepository checkpointRepository,
            Cashgame cashgame,
            EditCheckpointPostModel postModel,
            Checkpoint existingCheckpoint,
            TimeZoneInfo timeZone)
        {
            _checkpointRepository = checkpointRepository;
            _cashgame = cashgame;
            _postModel = postModel;
            _existingCheckpoint = existingCheckpoint;
            _timeZone = timeZone;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel)) return false;
            var postedCheckpoint = CreateCheckpoint(_postModel, _existingCheckpoint, _timeZone);
            _checkpointRepository.UpdateCheckpoint(_cashgame, postedCheckpoint);
            return true;
        }

        private Checkpoint CreateCheckpoint(EditCheckpointPostModel postModel, Checkpoint existingCheckpoint, TimeZoneInfo timeZone)
        {
            return CheckpointFactory.Create(
                TimeZoneInfo.ConvertTimeToUtc(postModel.Timestamp, timeZone),
                existingCheckpoint.Type,
                postModel.Stack,
                postModel.Amount,
                existingCheckpoint.Id);
        }
    }
}