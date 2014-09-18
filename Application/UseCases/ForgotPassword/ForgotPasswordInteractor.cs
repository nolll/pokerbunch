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

            SendMessage(messageSender, request.Email, password);

            var returnUrl = new ForgotPasswordConfirmationUrl();

            return new ForgotPasswordResult(returnUrl);
        }

        private static void SendMessage(IMessageSender messageSender, string email, string password)
        {
            var subject = PasswordMessageBuilder.GetSubject();
            var body = PasswordMessageBuilder.GetBody(password);

            messageSender.Send(email, subject, body);
        }
    }
}