using Core.Exceptions;
using Core.Repositories;

namespace Core.UseCases.RequireAdmin
{
    public class RequireAdminInteractor
    {
        private readonly IUserRepository _userRepository;

        public RequireAdminInteractor(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(RequireAdminRequest request)
        {
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            if(!user.IsAdmin)
                throw new AccessDeniedException();
        }
    }
}