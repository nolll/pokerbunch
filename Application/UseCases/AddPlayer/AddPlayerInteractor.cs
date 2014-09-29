using Application.Exceptions;
using Application.Urls;
using Core.Repositories;

namespace Application.UseCases.AddPlayer
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

            var player = playerRepository.GetByName(bunch, request.Name);
            if(player != null)
                throw new PlayerExistsException();

            playerRepository.Add(bunch, request.Name);

            var returnUrl = new AddPlayerConfirmationUrl(request.Slug);
            return new AddPlayerResult(returnUrl);
        }
    }
}