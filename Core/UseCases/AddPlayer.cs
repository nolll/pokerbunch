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
        private readonly IPlayerRepository _playerRepository;

        public AddPlayer(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var player = Player.NewWithoutUser(request.Slug, request.Name);
            _playerRepository.Add(player);

            return new Result(request.Slug);
        }

        public class Request
        {
            public string Slug { get; }
            public string Name { get; }

            public Request(string slug, string name)
            {
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