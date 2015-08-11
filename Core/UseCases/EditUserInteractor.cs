using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Repositories;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases.EditUser
{
    public class EditUserInteractor
    {
        private readonly IUserRepository _userRepository;

        public EditUserInteractor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public EditUserResult Execute(EditUserRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var userToSave = GetUser(user, request);
            
            _userRepository.Save(userToSave);

            var returnUrl = new UserDetailsUrl(request.UserName);
            return new EditUserResult(returnUrl);
        }

        private static User GetUser(User user, EditUserRequest request)
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

        public class EditUserRequest
        {
            public string UserName { get; private set; }
            [Required(ErrorMessage = "Display Name can't be empty")]
            public string DisplayName { get; private set; }
            public string RealName { get; private set; }
            [Required(ErrorMessage = "Email can't be empty")]
            [EmailAddress(ErrorMessage = "The email address is not valid")]
            public string Email { get; private set; }

            public EditUserRequest(string userName, string displayName, string realName, string email)
            {
                UserName = userName;
                DisplayName = displayName;
                RealName = realName;
                Email = email;
            }
        }

        public class EditUserResult
        {
            public Url ReturnUrl { get; private set; }

            public EditUserResult(Url returnUrl)
            {
                ReturnUrl = returnUrl;
            }
        }
    }
}
