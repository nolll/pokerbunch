using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases.ChangePassword
{
    public static class ChangePasswordInteractor
    {
        public static ChangePasswordResult Execute(IAuth auth, IUserRepository userRepository, IRandomService randomService, ChangePasswordRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            if (request.Password != request.Repeat)
                throw new ValidationException("The passwords dos not match");

            var salt = SaltGenerator.CreateSalt(randomService.GetAllowedChars());
            var encryptedPassword = EncryptionService.Encrypt(request.Password, salt);
            var user = userRepository.GetById(auth.CurrentIdentity.UserId);
            user = CreateUser(user, encryptedPassword, salt);
            
            userRepository.Save(user);

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
