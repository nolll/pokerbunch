using Core.Repositories;

namespace Core.UseCases.UserList
{
    public static  class UserListInteractor
    {
        public static UserListResult Execute(IUserRepository userRepository)
        {
            var users = userRepository.GetList();

            return new UserListResult(users);
        }
    }
}