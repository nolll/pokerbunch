﻿using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class Report
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly CashgameService _cashgameService;
        private readonly PlayerService _playerService;
        private readonly IUserRepository _userRepository;

        public Report(IBunchRepository bunchRepository, CashgameService cashgameService, PlayerService playerService, IUserRepository userRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameService = cashgameService;
            _playerService = playerService;
            _userRepository = userRepository;
        }

        public void Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.Get(request.Slug);
            var cashgame = _cashgameService.GetRunning(bunch.Id);
            var currentUser = _userRepository.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerService.GetByUser(bunch.Id, currentUser.Id);
            RequireRole.Me(currentUser, currentPlayer, request.PlayerId);

            var checkpoint = Checkpoint.Create(cashgame.Id, request.PlayerId, request.CurrentTime, CheckpointType.Report, request.Stack);
            cashgame.AddCheckpoint(checkpoint);
            _cashgameService.Update(cashgame);
        }

        public class Request
        {
            public string UserName { get; }
            public string Slug { get; }
            public string PlayerId { get; }
            [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
            public int Stack { get; }
            public DateTime CurrentTime { get; }

            public Request(string userName, string slug, string playerId, int stack, DateTime currentTime)
            {
                UserName = userName;
                Slug = slug;
                PlayerId = playerId;
                Stack = stack;
                CurrentTime = currentTime;
            }
        }
    }
}
