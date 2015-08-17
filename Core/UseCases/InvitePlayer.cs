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
        private readonly IUserRepository _userRepository;

        public InvitePlayer(IBunchRepository bunchRepository, IPlayerRepository playerRepository, IMessageSender messageSender, IUserRepository userRepository)
        {
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
            _messageSender = messageSender;
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var currentUser = _userRepository.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerRepository.GetByUserId(bunch.Id, currentUser.Id);
            RoleHandler.RequireManager(currentUser, currentPlayer);

            var player = _playerRepository.GetById(request.PlayerId);
            var message = new InvitationMessage(bunch, player, request.RegisterUrl);
            _messageSender.Send(request.Email, message);

            var url = new InvitePlayerConfirmationUrl(request.Slug, request.PlayerId);
            return new Result(url);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            public int PlayerId { get; private set; }
            [Required(ErrorMessage = "Email can't be empty")]
            [EmailAddress(ErrorMessage = "The email address is not valid")]
            public string Email { get; private set; }
            public string RegisterUrl { get; private set; }

            public Request(string userName, string slug, int playerId, string email, string registerUrl)
            {
                UserName = userName;
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