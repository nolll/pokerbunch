using System.ComponentModel.DataAnnotations;
using Core.Repositories;
using Core.Services;
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

            var player = _playerRepository.GetById(request.PlayerId);
            var bunch = _bunchRepository.GetById(player.BunchId);
            var currentUser = _userRepository.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerRepository.GetByUserId(bunch.Id, currentUser.Id);
            RoleHandler.RequireManager(currentUser, currentPlayer);

            var joinUrl = string.Format(request.JoinUrlFormat, bunch.Slug);
            var message = new InvitationMessage(bunch.DisplayName, player, request.RegisterUrl, joinUrl);
            _messageSender.Send(request.Email, message);

            return new Result(player.Id);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public int PlayerId { get; private set; }
            [Required(ErrorMessage = "Email can't be empty")]
            [EmailAddress(ErrorMessage = "The email address is not valid")]
            public string Email { get; private set; }
            public string RegisterUrl { get; private set; }
            public string JoinUrlFormat { get; private set; }

            public Request(string userName, int playerId, string email, string registerUrl, string joinUrlFormat)
            {
                UserName = userName;
                PlayerId = playerId;
                Email = email;
                RegisterUrl = registerUrl;
                JoinUrlFormat = joinUrlFormat;
            }
        }

        public class Result
        {
            public int PlayerId { get; private set; }

            public Result(int playerId)
            {
                PlayerId = playerId;
            }
        }
    }
}