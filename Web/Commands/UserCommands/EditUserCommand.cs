using Core.Entities;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.UserModels.Edit;

namespace Web.Commands.UserCommands
{
    public class EditUserCommand : Command
    {
        private readonly IUserRepository _userRepository;
        private readonly User _user;
        private readonly EditUserPostModel _postModel;

        public EditUserCommand(
            IUserRepository userRepository,
            User user, 
            EditUserPostModel postModel)
        {
            _userRepository = userRepository;
            _user = user;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel)) return false;
            var userToSave = UserModelMapper.GetUser(_user, _postModel);
            _userRepository.Save(userToSave);
            return true;
        }
    }
}