using Core.Entities;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.HomegameModels.Edit;

namespace Web.Commands.HomegameCommands
{
    public class EditHomegameCommand : Command
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly Bunch _bunch;
        private readonly HomegameEditPostModel _postModel;

        public EditHomegameCommand(
            IBunchRepository bunchRepository,
            Bunch bunch, 
            HomegameEditPostModel postModel)
        {
            _bunchRepository = bunchRepository;
            _bunch = bunch;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel)) return false;
            var postedHomegame = HomegameModelMapper.GetHomegame(_bunch, _postModel);
            _bunchRepository.Save(postedHomegame);
            return false;
        }
    }
}