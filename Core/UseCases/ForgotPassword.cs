using System.ComponentModel.DataAnnotations;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class ForgotPassword
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageSender _messageSender;
        private readonly IRandomService _randomService;

        public ForgotPassword(IUserRepository userRepository, IMessageSender messageSender, IRandomService randomService)
        {
            _userRepository = userRepository;
            _messageSender = messageSender;
            _randomService = randomService;
        }

        public Result Execute(Request request)
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

            return new Result(returnUrl);
        }

        public class Request
        {
            [Required(ErrorMessage = "Email can't be empty")]
            [EmailAddress(ErrorMessage = "The email address is not valid")]
            public string Email { get; private set; }

            public Request(string email)
            {
                Email = email;
            }
        }

        public class Result
        {
            public Url ReturnUrl { get; private set; }

            public Result(Url returnUrl)
            {
                ReturnUrl = returnUrl;
            }
        }

        private class ForgotPasswordMessage : IMessage
        {
            private readonly string _password;

            public ForgotPasswordMessage(string password)
            {
                _password = password;
            }

            public string Subject
            {
                get { return "Poker Bunch Password Recovery"; }
            }

            public string Body
            {
                get
                {
                    var loginUrl = new LoginUrl().Absolute;
                    return string.Format(BodyFormat, _password, loginUrl);
                }
            }

            private const string BodyFormat =
@"Here is your new password for Poker Bunch:
{0}

Please sign in here: {1}";
        }
    }
}