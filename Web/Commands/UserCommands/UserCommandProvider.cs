using Core.Repositories;
using Core.Services;
using Core.Services.Interfaces;
using Web.Models.UserModels.ChangePassword;
using Web.Models.UserModels.Edit;

namespace Web.Commands.UserCommands
{
    public class UserCommandProvider : IUserCommandProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuth _auth;
        private readonly IRandomService _randomService;

        public UserCommandProvider(
            IUserRepository userRepository,
            IAuth auth,
            IRandomService randomService)
        {
            _userRepository = userRepository;
            _auth = auth;
            _randomService = randomService;
        }

        public Command GetChangePasswordCommand(ChangePasswordPostModel postModel)
        {
            var user = _auth.CurrentUser;

            return new ChangePasswordCommand(
                _userRepository,
                _randomService,
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