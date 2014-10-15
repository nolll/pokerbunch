using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.EditUser
{
    public static class EditUserInteractor
    {
        public static EditUserResult Execute(IUserRepository userRepository, EditUserRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var user = userRepository.GetByNameOrEmail(request.UserName);
            var userToSave = GetUser(user, request);
            userRepository.Save(userToSave);

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
