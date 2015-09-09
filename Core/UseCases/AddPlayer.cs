using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class AddPlayer
    {
        private readonly BunchService _bunchService;
        private readonly IPlayerRepository _playerRepository;
        private readonly UserService _userService;

        public AddPlayer(BunchService bunchService, IPlayerRepository playerRepository, UserService userService)
        {
            _bunchService = bunchService;
            _playerRepository = playerRepository;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchService.GetBySlug(request.Slug);
            var currentUser = _userService.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerRepository.GetByUserId(bunch.Id, currentUser.Id);
            RoleHandler.RequireManager(currentUser, currentPlayer);
            var existingPlayers = _playerRepository.GetList(bunch.Id);
            var player = existingPlayers.FirstOrDefault(o => String.Equals(o.DisplayName, request.Name, StringComparison.CurrentCultureIgnoreCase));
            if(player != null)
                throw new PlayerExistsException();

            player = new Player(bunch.Id, request.Name, Role.Player);
            _playerRepository.Add(player);

            return new Result(bunch.Slug);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            [Required(ErrorMessage = "Name can't be empty")]
            public string Name { get; private set; }

            public Request(string userName, string slug, string name)
            {
                UserName = userName;
                Slug = slug;
                Name = name;
            }
        }

        public class Result
        {
            public string Slug { get; private set; }

            public Result(string slug)
            {
                Slug = slug;
            }
        }
    }
}