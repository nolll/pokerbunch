using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases.InvitePlayer
{
    public class InvitePlayerInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMessageSender _messageSender;

        public InvitePlayerInteractor(IBunchRepository bunchRepository, IPlayerRepository playerRepository, IMessageSender messageSender)
        {
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
            _messageSender = messageSender;
        }

        public InvitePlayerResult Execute(InvitePlayerRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var player = _playerRepository.GetById(request.PlayerId);
            var message = new InvitationMessage(bunch, player);
            _messageSender.Send(request.Email, message);

            var url = new InvitePlayerConfirmationUrl(request.Slug, request.PlayerId);
            return new InvitePlayerResult(url);
        }
    }
}