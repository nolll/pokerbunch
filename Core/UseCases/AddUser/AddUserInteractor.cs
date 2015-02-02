using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.AddUser
{
    public class AddUserInteractor
    {
        private readonly IUserRepository _userRepository;
        private readonly IRandomService _randomService;
        private readonly IMessageSender _messageSender;

        public AddUserInteractor(
            IUserRepository userRepository,
            IRandomService randomService,
            IMessageSender messageSender)
        {
            _userRepository = userRepository;
            _randomService = randomService;
            _messageSender = messageSender;
        }

        public AddUserResult Execute(AddUserRequest request)
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

            return new AddUserResult();
        }

        private static User CreateUser(AddUserRequest request, string encryptedPassword, string salt)
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
    }
}