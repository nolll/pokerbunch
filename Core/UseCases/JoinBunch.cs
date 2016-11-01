﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class JoinBunch
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly PlayerService _playerService;
        private readonly IUserRepository _userRepository;

        public JoinBunch(IBunchRepository bunchRepository, PlayerService playerService, IUserRepository userRepository)
        {
            _bunchRepository = bunchRepository;
            _playerService = playerService;
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.Get(request.Slug);
            var players = _playerService.List(bunch.Id);
            var player = GetMatchedPlayer(players, request.Code);
            
            if (player == null)
                throw new InvalidJoinCodeException();

            var user = _userRepository.GetByNameOrEmail(request.UserName);
            _playerService.JoinBunch(player, bunch, user.Id);
            return new Result(bunch.Id, player.Id);
        }
        
        private static Player GetMatchedPlayer(IEnumerable<Player> players, string postedCode)
        {
            foreach (var player in players)
            {
                var code = InvitationCodeCreator.GetCode(player);
                if (code == postedCode)
                    return player;
            }
            return null;
        }

        public class Request
        {
            public string Slug { get; }
            public string UserName { get; }
            [Required(ErrorMessage = "Code can't be empty")]
            public string Code { get; }

            public Request(string slug, string userName, string code)
            {
                Slug = slug;
                UserName = userName;
                Code = code;
            }
        }

        public class Result
        {
            public string Slug { get; private set; }
            public string PlayerId { get; private set; }

            public Result(string slug, string playerId)
            {
                Slug = slug;
                PlayerId = playerId;
            }
        }
    }
}
