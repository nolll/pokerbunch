using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class AddPlayer
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;

        public AddPlayer(IBunchRepository bunchRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);

            var existingPlayers = _playerRepository.GetList(bunch.Id);
            var player = existingPlayers.FirstOrDefault(o => String.Equals(o.DisplayName, request.Name, StringComparison.CurrentCultureIgnoreCase));
            if(player != null)
                throw new PlayerExistsException();

            player = new Player(bunch.Id, request.Name, Role.Player);
            _playerRepository.Add(player);

            var returnUrl = new AddPlayerConfirmationUrl(request.Slug);
            return new Result(returnUrl);
        }

        public class Request
        {
            public string Slug { get; private set; }
            [Required(ErrorMessage = "Name can't be empty")]
            public string Name { get; private set; }

            public Request(string slug, string name)
            {
                Slug = slug;
                Name = name;
            }
        }

        public class Result
        {
            public AddPlayerConfirmationUrl ReturnUrl { get; private set; }

            public Result(AddPlayerConfirmationUrl returnUrl)
            {
                ReturnUrl = returnUrl;
            }
        }
    }
}