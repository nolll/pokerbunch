using System.ComponentModel.DataAnnotations;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class InvitePlayer
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMessageSender _messageSender;

        public InvitePlayer(IBunchRepository bunchRepository, IPlayerRepository playerRepository, IMessageSender messageSender)
        {
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
            _messageSender = messageSender;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var player = _playerRepository.GetById(request.PlayerId);
            var message = new InvitationMessage(bunch, player, request.RegisterUrl);
            _messageSender.Send(request.Email, message);

            var url = new InvitePlayerConfirmationUrl(request.Slug, request.PlayerId);
            return new Result(url);
        }

        public class Request
        {
            public string Slug { get; private set; }
            public int PlayerId { get; private set; }
            [Required(ErrorMessage = "Email can't be empty")]
            [EmailAddress(ErrorMessage = "The email address is not valid")]
            public string Email { get; private set; }
            public string RegisterUrl { get; private set; }

            public Request(string slug, int playerId, string email, string registerUrl)
            {
                Slug = slug;
                PlayerId = playerId;
                Email = email;
                RegisterUrl = registerUrl;
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