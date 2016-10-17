using System.ComponentModel.DataAnnotations;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class InvitePlayer
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly PlayerService _playerService;
        private readonly IMessageSender _messageSender;
        private readonly UserService _userService;

        public InvitePlayer(IBunchRepository bunchRepository, PlayerService playerService, IMessageSender messageSender, UserService userService)
        {
            _bunchRepository = bunchRepository;
            _playerService = playerService;
            _messageSender = messageSender;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var player = _playerService.Get(request.PlayerId);
            var bunch = _bunchRepository.Get(player.BunchId);
            var currentUser = _userService.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerService.GetByUserId(bunch.Id, currentUser.Id);
            RequireRole.Manager(currentUser, currentPlayer);

            var invitationCode = InvitationCodeCreator.GetCode(player);
            var joinUrl = string.Format(request.JoinUrlFormat, bunch.Id);
            var joinWithCodeUrl = string.Format(request.JoinWithCodeUrlFormat, bunch.Id, invitationCode);
            var message = new InvitationMessage(bunch.DisplayName, invitationCode, request.RegisterUrl, joinUrl, joinWithCodeUrl);
            _messageSender.Send(request.Email, message);

            return new Result(player.Id);
        }

        public class Request
        {
            public string UserName { get; }
            public string PlayerId { get; }
            [Required(ErrorMessage = "Email can't be empty")]
            [EmailAddress(ErrorMessage = "The email address is not valid")]
            public string Email { get; }
            public string RegisterUrl { get; }
            public string JoinUrlFormat { get; }
            public string JoinWithCodeUrlFormat { get; }

            public Request(string userName, string playerId, string email, string registerUrl, string joinUrlFormat, string joinWithCodeUrlFormat)
            {
                UserName = userName;
                PlayerId = playerId;
                Email = email;
                RegisterUrl = registerUrl;
                JoinUrlFormat = joinUrlFormat;
                JoinWithCodeUrlFormat = joinWithCodeUrlFormat;
            }
        }

        public class Result
        {
            public string PlayerId { get; private set; }

            public Result(string playerId)
            {
                PlayerId = playerId;
            }
        }
    }
}