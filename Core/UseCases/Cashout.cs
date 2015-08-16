﻿using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities.Checkpoints;
using Core.Repositories;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class Cashout
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICheckpointRepository _checkpointRepository;

        public Cashout(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository, ICheckpointRepository checkpointRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _checkpointRepository = checkpointRepository;
        }

        public void Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var player = _playerRepository.GetById(request.PlayerId);
            var cashgame = _cashgameRepository.GetRunning(bunch.Id);
            var result = cashgame.GetResult(player.Id);

            var existingCashoutCheckpoint = result.CashoutCheckpoint;
            var postedCheckpoint = Checkpoint.Create(
                cashgame.Id,
                player.Id,
                request.CurrentTime,
                CheckpointType.Cashout,
                request.Stack,
                0,
                existingCashoutCheckpoint != null ? existingCashoutCheckpoint.Id : 0);

            if (existingCashoutCheckpoint != null)
                _checkpointRepository.UpdateCheckpoint(postedCheckpoint);
            else
                _checkpointRepository.AddCheckpoint(postedCheckpoint);
        }

        public class Request
        {
            public string Slug { get; private set; }
            public int PlayerId { get; private set; }
            [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
            public int Stack { get; private set; }
            public DateTime CurrentTime { get; private set; }

            public Request(string slug, int playerId, int stack, DateTime currentTime)
            {
                Slug = slug;
                PlayerId = playerId;
                Stack = stack;
                CurrentTime = currentTime;
            }
        }
    }
}