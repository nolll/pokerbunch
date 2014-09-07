using Core.Repositories;

namespace Application.UseCases.EditUserForm
{
    public static class EditUserFormInteractor
    {
        public static EditUserFormResult Execute(IUserRepository userRepository, EditUserFormRequest request)
        {
            var user = userRepository.GetByNameOrEmail(request.UserName);
            var userName = user.UserName;
            var realName = user.RealName;
            var displayName = user.DisplayName;
            var email = user.Email;

            return new EditUserFormResult(userName, realName, displayName, email);
        }
    }
}