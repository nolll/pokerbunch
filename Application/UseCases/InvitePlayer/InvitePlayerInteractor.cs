using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Application.Urls;
using Core.Repositories;

namespace Application.UseCases.InvitePlayer
{
    public class InvitePlayerInteractor : IInvitePlayerInteractor
    {
        private readonly IMessageSender _messageSender;
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;

        public InvitePlayerInteractor(
            IMessageSender messageSender,
            IBunchRepository bunchRepository,
            IPlayerRepository playerRepository)
        {
            _messageSender = messageSender;
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
        }

        public InvitePlayerResult Execute(InvitePlayerRequest request)
        {
            var validator = new Validator(request);

            var success = false;
            var errors = new List<string>();

            if (validator.IsValid)
            {
                success = true;
                SendMessage(request);
            }
            else
            {
                errors = validator.Errors.ToList();
            }

            var url = new InvitePlayerConfirmationUrl(request.Slug, request.PlayerId);

            return new InvitePlayerResult(success, errors, url);
        }

        private void SendMessage(InvitePlayerRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var player = _playerRepository.GetById(request.PlayerId);
            var subject = InvitationMessageBuilder.GetSubject(bunch);
            var body = InvitationMessageBuilder.GetBody(bunch, player);
            _messageSender.Send(request.Email, subject, body);
        }
    }
}