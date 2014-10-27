using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.AddUser
{
    public static class AddUserInteractor
    {
        public static AddUserResult Execute(
            IUserRepository userRepository,
            IRandomService randomService,
            IMessageSender messageSender,
            AddUserRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            if (userRepository.GetByNameOrEmail(request.UserName) != null)
                throw new UserExistsException();

            if (userRepository.GetByNameOrEmail(request.Email) != null)
                throw new EmailExistsException();

            var password = PasswordGenerator.CreatePassword(randomService.GetAllowedChars());
            var salt = SaltGenerator.CreateSalt(randomService.GetAllowedChars());
            var encryptedPassword = EncryptionService.Encrypt(password, salt);
            var user = CreateUser(request, encryptedPassword, salt);

            userRepository.Add(user);
            
            var message = new RegistrationMessage(password);
            messageSender.Send(request.Email, message);

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