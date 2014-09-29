using Application.Exceptions;
using Core.Repositories;

namespace Application.UseCases.AddUser
{
    public static class AddUserInteractor
    {
        public static AddUserResult Execute(
            IUserRepository userRepository,
            AddUserRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            if (userRepository.GetByNameOrEmail(request.UserName) != null)
                throw new UserExistsException();

            if (userRepository.GetByNameOrEmail(request.Email) != null)
                throw new EmailExistsException();

            return new AddUserResult();
        }
    }
}