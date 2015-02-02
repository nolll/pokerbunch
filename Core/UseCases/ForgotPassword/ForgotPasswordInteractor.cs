using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases.ForgotPassword
{
    public class ForgotPasswordInteractor
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageSender _messageSender;
        private readonly IRandomService _randomService;

        public ForgotPasswordInteractor(IUserRepository userRepository, IMessageSender messageSender, IRandomService randomService)
        {
            _userRepository = userRepository;
            _messageSender = messageSender;
            _randomService = randomService;
        }

        public ForgotPasswordResult Execute(ForgotPasswordRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var user = _userRepository.GetByNameOrEmail(request.Email);
            if(user == null)
                throw new UserNotFoundException();

            var password = PasswordGenerator.CreatePassword(_randomService.GetAllowedChars());
            var salt = SaltGenerator.CreateSalt(_randomService.GetAllowedChars());
            var encryptedPassword = EncryptionService.Encrypt(password, salt);

            user.SetPassword(encryptedPassword, salt);
            
            _userRepository.Save(user);
            
            var message = new ForgotPasswordMessage(password);
            _messageSender.Send(request.Email, message);

            var returnUrl = new ForgotPasswordConfirmationUrl();

            return new ForgotPasswordResult(returnUrl);
        }
    }
}