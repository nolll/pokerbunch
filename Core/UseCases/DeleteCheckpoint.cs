﻿using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases
{
    public class DeleteCheckpoint
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public DeleteCheckpoint(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, ICheckpointRepository checkpointRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _checkpointRepository = checkpointRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequireManager(user, player);
            var cashgame = _cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            var checkpoint = _checkpointRepository.GetCheckpoint(request.CheckpointId);
            _checkpointRepository.DeleteCheckpoint(checkpoint);

            var returnUrl = GetReturnUrl(cashgame.Status, request, cashgame);
            return new Result(returnUrl);
        }

        private static Url GetReturnUrl(GameStatus status, Request request, Cashgame cashgame)
        {
            if(status == GameStatus.Running)
                return new RunningCashgameUrl(request.Slug);
            return new CashgameDetailsUrl(cashgame.Id);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            public string DateStr { get; private set; }
            public int CheckpointId { get; private set; }

            public Request(string userName, string slug, string dateStr, int checkpointId)
            {
                UserName = userName;
                Slug = slug;
                DateStr = dateStr;
                CheckpointId = checkpointId;
            }
        }

        public class Result
        {
            public Url ReturnUrl { get; private set; }

            public Result(Url returnUrl)
            {
                ReturnUrl = returnUrl;
            }
        }
    }
}
