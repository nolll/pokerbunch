using Core.Classes;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.UserModels.Edit;

namespace Web.Commands.UserCommands
{
    public class EditUserCommand : Command
    {
        private readonly IUserModelMapper _userModelMapper;
        private readonly IUserRepository _userRepository;
        private readonly User _user;
        private readonly EditUserPostModel _postModel;

        public EditUserCommand(
            IUserModelMapper userModelMapper,
            IUserRepository userRepository,
            User user, 
            EditUserPostModel postModel)
        {
            _userModelMapper = userModelMapper;
            _userRepository = userRepository;
            _user = user;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel)) return false;
            var userToSave = _userModelMapper.GetUser(_user, _postModel);
            _userRepository.UpdateUser(userToSave);
            return true;
        }
    }
}