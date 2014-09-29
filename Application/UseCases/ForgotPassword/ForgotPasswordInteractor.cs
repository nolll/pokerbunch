using Application.Exceptions;
using Application.Services;
using Application.Urls;
using Core.Repositories;

namespace Application.UseCases.ForgotPassword
{
    public static class ForgotPasswordInteractor
    {
        public static ForgotPasswordResult Execute(
            IUserRepository userRepository,
            IMessageSender messageSender,
            IRandomService randomService,
            ForgotPasswordRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var user = userRepository.GetByNameOrEmail(request.Email);
            if(user == null)
                throw new UserNotFoundException();

            var password = PasswordGenerator.CreatePassword(randomService.GetPasswordCharacters());
            var salt = SaltGenerator.CreateSalt(randomService.GetPasswordCharacters());
            var encryptedPassword = EncryptionService.Encrypt(password, salt);

            user.SetPassword(encryptedPassword, salt);
            userRepository.Save(user);

            var message = new ForgotPasswordMessage(password);
            messageSender.Send(request.Email, message);

            var returnUrl = new ForgotPasswordConfirmationUrl();

            return new ForgotPasswordResult(returnUrl);
        }
    }
}