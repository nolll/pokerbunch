using Core.Repositories;

namespace Application.UseCases.EditUserForm
{
    public class EditUserFormInteractor : IEditUserFormInteractor
    {
        private readonly IUserRepository _userRepository;

        public EditUserFormInteractor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public EditUserFormResult Execute(EditUserFormRequest request)
        {
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var userName = user.UserName;
            var realName = user.RealName;
            var displayName = user.DisplayName;
            var email = user.Email;

            return new EditUserFormResult(userName, realName, displayName, email);
        }
    }
}