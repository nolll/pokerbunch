using Application.Services;
using Core.Repositories;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;

namespace Web.Commands.UserCommands
{
    public class UserCommandProvider : IUserCommandProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuth _auth;

        public UserCommandProvider(
            IUserRepository userRepository,
            IAuth auth)
        {
            _userRepository = userRepository;
            _auth = auth;
        }

        public Command GetChangePasswordCommand(ChangePasswordPostModel postModel)
        {
            var user = _auth.CurrentUser;

            return new ChangePasswordCommand(
                _userRepository,
                user, 
                postModel);
        }

        public Command GetEditCommand(string userName, EditUserPostModel postModel)
        {
            var user = _userRepository.GetByNameOrEmail(userName);

            return new EditUserCommand(
                _userRepository,
                user, 
                postModel);
        }
    }
}