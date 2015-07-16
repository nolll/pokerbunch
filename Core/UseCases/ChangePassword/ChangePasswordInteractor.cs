using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases.ChangePassword
{
    public class ChangePasswordInteractor
    {
        private readonly IUserRepository _userRepository;
        private readonly IRandomService _randomService;

        public ChangePasswordInteractor(IUserRepository userRepository, IRandomService randomService)
        {
            _userRepository = userRepository;
            _randomService = randomService;
        }

        public ChangePasswordResult Execute(ChangePasswordRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            if (request.Password != request.Repeat)
                throw new ValidationException("The passwords dos not match");

            var salt = SaltGenerator.CreateSalt(_randomService.GetAllowedChars());
            var encryptedPassword = EncryptionService.Encrypt(request.Password, salt);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            user = CreateUser(user, encryptedPassword, salt);
            
            _userRepository.Save(user);

            var returnUrl = new ChangePasswordConfirmationUrl();
            return new ChangePasswordResult(returnUrl);
        }

        private static User CreateUser(User user, string encryptedPassword, string salt)
        {
            return new User(
                user.Id,
                user.UserName,
                user.DisplayName,
                user.RealName,
                user.Email,
                user.GlobalRole,
                encryptedPassword,
                salt);
        }
    }
}
