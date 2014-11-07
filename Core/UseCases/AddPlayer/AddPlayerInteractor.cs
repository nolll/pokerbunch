using System;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.AddPlayer
{
    public static class AddPlayerInteractor
    {
        public static AddPlayerResult Execute(
            IBunchRepository bunchRepository,
            IPlayerRepository playerRepository,
            AddPlayerRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = bunchRepository.GetBySlug(request.Slug);

            var existingPlayers = playerRepository.GetList(bunch.Id);
            var player = existingPlayers.FirstOrDefault(o => String.Equals(o.DisplayName, request.Name, StringComparison.CurrentCultureIgnoreCase));
            if(player != null)
                throw new PlayerExistsException();

            player = new Player(bunch.Id, request.Name, Role.Player);
            playerRepository.Add(player);

            var returnUrl = new AddPlayerConfirmationUrl(request.Slug);
            return new AddPlayerResult(returnUrl);
        }
    }
}