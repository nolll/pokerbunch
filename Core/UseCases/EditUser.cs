using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Repositories;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class EditUser
    {
        private readonly IUserRepository _userRepository;

        public EditUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var userToSave = GetUser(user, request);
            
            _userRepository.Save(userToSave);

            var returnUrl = new UserDetailsUrl(request.UserName);
            return new Result(returnUrl);
        }

        private static User GetUser(User user, Request request)
        {
            return new User(
                user.Id,
                user.UserName,
                request.DisplayName,
                request.RealName,
                request.Email,
                user.GlobalRole,
                user.EncryptedPassword,
                user.Salt);
        }

        public class Request
        {
            public string UserName { get; private set; }
            [Required(ErrorMessage = "Display Name can't be empty")]
            public string DisplayName { get; private set; }
            public string RealName { get; private set; }
            [Required(ErrorMessage = "Email can't be empty")]
            [EmailAddress(ErrorMessage = "The email address is not valid")]
            public string Email { get; private set; }

            public Request(string userName, string displayName, string realName, string email)
            {
                UserName = userName;
                DisplayName = displayName;
                RealName = realName;
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
    }
}
