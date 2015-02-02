using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Urls;

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
    }
}
