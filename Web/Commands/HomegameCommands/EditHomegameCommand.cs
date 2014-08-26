using Core.Entities;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.HomegameModels.Edit;

namespace Web.Commands.HomegameCommands
{
    public class EditHomegameCommand : Command
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly Homegame _homegame;
        private readonly HomegameEditPostModel _postModel;

        public EditHomegameCommand(
            IHomegameRepository homegameRepository,
            Homegame homegame, 
            HomegameEditPostModel postModel)
        {
            _homegameRepository = homegameRepository;
            _homegame = homegame;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel)) return false;
            var postedHomegame = HomegameModelMapper.GetHomegame(_homegame, _postModel);
            _homegameRepository.Save(postedHomegame);
            return false;
        }
    }
}