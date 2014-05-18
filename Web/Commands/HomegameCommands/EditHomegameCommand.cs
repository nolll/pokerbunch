using Core.Entities;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.HomegameModels.Edit;

namespace Web.Commands.HomegameCommands
{
    public class EditHomegameCommand : Command
    {
        private readonly IHomegameModelMapper _homegameModelMapper;
        private readonly IHomegameRepository _homegameRepository;
        private readonly Homegame _homegame;
        private readonly HomegameEditPostModel _postModel;

        public EditHomegameCommand(
            IHomegameModelMapper homegameModelMapper,
            IHomegameRepository homegameRepository,
            Homegame homegame, 
            HomegameEditPostModel postModel)
        {
            _homegameModelMapper = homegameModelMapper;
            _homegameRepository = homegameRepository;
            _homegame = homegame;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel)) return false;
            var postedHomegame = _homegameModelMapper.GetHomegame(_homegame, _postModel);
            _homegameRepository.Save(postedHomegame);
            return false;
        }
    }
}