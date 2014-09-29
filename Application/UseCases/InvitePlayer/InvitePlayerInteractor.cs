using Application.Exceptions;
using Application.Services;
using Application.Urls;
using Core.Repositories;

namespace Application.UseCases.InvitePlayer
{
    public static class InvitePlayerInteractor
    {
        public static InvitePlayerResult Execute(IBunchRepository bunchRepository, IPlayerRepository playerRepository, IMessageSender messageSender, InvitePlayerRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = bunchRepository.GetBySlug(request.Slug);
            var player = playerRepository.GetById(request.PlayerId);
            var message = new InvitationMessage(bunch, player);
            messageSender.Send(request.Email, message);

            var url = new InvitePlayerConfirmationUrl(request.Slug, request.PlayerId);
            return new InvitePlayerResult(url);
        }
    }
}