using Core.Repositories;

namespace Core.UseCases.EditUserForm
{
    public class EditUserFormInteractor
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

        public class EditUserFormRequest
        {
            public string UserName { get; private set; }

            public EditUserFormRequest(string userName)
            {
                UserName = userName;
            }
        }

        public class EditUserFormResult
        {
            public string UserName { get; private set; }
            public string RealName { get; private set; }
            public string DisplayName { get; private set; }
            public string Email { get; private set; }

            public EditUserFormResult(string userName, string realName, string displayName, string email)
            {
                UserName = userName;
                RealName = realName;
                DisplayName = displayName;
                Email = email;
            }
        }
    }
}