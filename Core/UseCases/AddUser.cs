using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class AddUser
    {
        private readonly IUserRepository _userRepository;
        private readonly IRandomService _randomService;
        private readonly IMessageSender _messageSender;

        public AddUser(
            IUserRepository userRepository,
            IRandomService randomService,
            IMessageSender messageSender)
        {
            _userRepository = userRepository;
            _randomService = randomService;
            _messageSender = messageSender;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            if (_userRepository.GetByNameOrEmail(request.UserName) != null)
                throw new UserExistsException();

            if (_userRepository.GetByNameOrEmail(request.Email) != null)
                throw new EmailExistsException();

            var password = PasswordGenerator.CreatePassword(_randomService.GetAllowedChars());
            var salt = SaltGenerator.CreateSalt(_randomService.GetAllowedChars());
            var encryptedPassword = EncryptionService.Encrypt(password, salt);
            var user = CreateUser(request, encryptedPassword, salt);

            _userRepository.Add(user);
            
            var message = new RegistrationMessage(password);
            _messageSender.Send(request.Email, message);

            return new Result();
        }

        private static User CreateUser(Request request, string encryptedPassword, string salt)
        {
            return new User(
                0,
                request.UserName,
                request.DisplayName,
                string.Empty,
                request.Email,
                Role.Player,
                encryptedPassword,
                salt);
        }

        public class Request
        {
            [Required(ErrorMessage = "Login Name can't be empty")]
            public string UserName { get; private set; }

            [Required(ErrorMessage = "Display Name can't be empty")]
            public string DisplayName { get; private set; }

            [Required(ErrorMessage = "Email can't be empty")]
            [EmailAddress(ErrorMessage = "The email address is not valid")]
            public string Email { get; private set; }

            public Request(string userName, string displayName, string email)
            {
                UserName = userName;
                DisplayName = displayName;
                Email = email;
            }
        }

        public class Result
        {
            public Url ReturnUrl { get; private set; }

            public Result()
            {
                ReturnUrl = new AddUserConfirmationUrl();
            }
        }
    }
}