using System;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.Commands.CashgameCommands
{
    public class EditCheckpointCommand : Command
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly ICheckpointModelMapper _checkpointModelMapper;
        private readonly Cashgame _cashgame;
        private readonly EditCheckpointPostModel _postModel;
        private readonly Checkpoint _existingCheckpoint;
        private readonly TimeZoneInfo _timeZone;

        public EditCheckpointCommand(
            ICheckpointRepository checkpointRepository,
            ICheckpointModelMapper checkpointModelMapper,
            Cashgame cashgame,
            EditCheckpointPostModel postModel,
            Checkpoint existingCheckpoint,
            TimeZoneInfo timeZone)
        {
            _checkpointRepository = checkpointRepository;
            _checkpointModelMapper = checkpointModelMapper;
            _cashgame = cashgame;
            _postModel = postModel;
            _existingCheckpoint = existingCheckpoint;
            _timeZone = timeZone;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel)) return false;
            var postedCheckpoint = _checkpointModelMapper.GetCheckpoint(_postModel, _existingCheckpoint, _timeZone);
            _checkpointRepository.UpdateCheckpoint(_cashgame, postedCheckpoint);
            return true;
        }
    }
}